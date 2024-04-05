using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BusketManager;
using SmartShop.BasketService.Infrastructure;
using SmartShop.BasketService.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SmartShop.BasketService.Controllers
{
    [Route("Busket")]
    [ApiController]
    public class BusketController: ControllerBase
    {
        private IHttpContextAccessor contextAccessor;
        private IBusketSaver saver;

        public BusketController(IHttpContextAccessor contextAccessor, IBusketSaver saver)
        {
            this.saver = saver;
            this.contextAccessor = contextAccessor;
        }

        [HttpGet("BusketProducts")]
        public IActionResult GetProductsInBusket()
        {
            //Busket busket = contextAccessor.HttpContext.Session.GetJson<Busket>("line");
            //if (busket != null)
            //{
                //if (busket.BusketLines.Count() != 0)
                //{
                    return Ok(saver.SetBusket());
                //}
            //}

            //return BadRequest("No data");
        }

        [HttpGet("Add/{id}")]
        public IActionResult SetProductToBusket(long id, string redir)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
                .Create($"http://localhost:5300/Products/ForBusket/{id}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseString = string.Empty;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseString = reader.ReadToEnd();
                }
            }

            if (!string.IsNullOrEmpty(responseString))
            {
                BusketProduct busketProduct = JsonConvert.DeserializeObject<BusketProduct>(responseString);

                if (busketProduct != null)
                {
                    Busket busket = contextAccessor.HttpContext.Session.GetJson<Busket>("line");
                    if (busket == null)
                    {
                        busket = new Busket();
                    }
                    busket.AddBusketLine(busketProduct, 1);
                    saver.GetBusket(busket);
                    contextAccessor.HttpContext.Session.SetJson<Busket>("line", busket);
                    return Redirect(redir);
                    
                }
            }

            return BadRequest();

        }

        [HttpGet("Delete/{id}")]
        public IActionResult RemoveFromBusket(long id)
        {
            List<BusketLine> lines = saver.SetBusket();
            try
            {
                lines.Remove(lines.First(l => l.BusketProduct.Id == id));
            }
            catch (Exception)
            {
                saver.GetBusket(new Busket() { BusketLines = lines });
            }
            saver.GetBusket(new Busket() {BusketLines = lines });
            return Ok();
        }

        [HttpGet("Delete/All")]
        public IActionResult RemoveAll()
        {
            List<BusketLine> lines = saver.SetBusket();
            lines.Clear();
            saver.GetBusket(new Busket() { BusketLines = lines });
            return Ok();
        }
    }
}

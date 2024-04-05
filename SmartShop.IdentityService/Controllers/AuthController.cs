using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using IdentityManager.Ifrastructure;
using IdentityManager.Models;
using Microsoft.AspNetCore.Http;

namespace SmartShop.IdentityService.Controllers
{
    [ApiController]
    [Route("Authentication")]
    public class AuthController : ControllerBase
    {
        private ITokenService tokenService;

        public AuthController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpGet("Login")]
        public IActionResult Login(string name, string password)
        {
            if (name!= null && password!= null)
            {
                string ResponseString = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.
                    Create($"http://localhost:5100/Account/GetUser?name={name}&password={password}");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        ResponseString = reader.ReadToEnd();
                    }
                }

                if (!string.IsNullOrEmpty(ResponseString))
                {
                    RequestUser user = JsonConvert.DeserializeObject<RequestUser>(ResponseString);
                    
                    string token = tokenService.CreateToken(user);
                    ResponseUser respuser = new ResponseUser { Email = user.Email, Name = user.Name, Token = token };
                    
                    return Ok(respuser);
                }
            }

            ModelState.AddModelError("Uncorrect Data: ","Invalid login or Password");
            return Redirect("/SmartShop/Login");
        }
    }

}

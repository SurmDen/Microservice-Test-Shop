using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using IdentityManager.Models;


namespace OcelotApiGateway.Infrastructure
{
    public class JwtTokenMiddleware
    {
        private RequestDelegate next;
        public JwtTokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string responseModel = string.Empty;
            if (context.Request.Path == "/login")
            {
                string name = context.Request.Query["name"];
                string password = context.Request.Query["password"];

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.
                        Create($"http://localhost:5200/Authentication/Login?name={name}&password={password}");

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                     

                     using (Stream stream = response.GetResponseStream())
                     {
                         using (StreamReader reader = new StreamReader(stream))
                         {
                            responseModel = await reader.ReadToEndAsync();
                         }
                     }

                    ResponseUser user = JsonConvert.DeserializeObject<ResponseUser>(responseModel);

                    context.Session.SetString("token", user.Token);
                    context.Session.SetString("name", user.Name);
                    context.Session.SetString("email", user.Email);

                    await next.Invoke(context);

                }
                else
                {
                    context.Response.Redirect("/ErrorPage");
                }
            }
            else
            {
                await next.Invoke(context);
            }

            

        }
    }
}

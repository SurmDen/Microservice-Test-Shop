using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotApiGateway.Infrastructure
{
    public interface ITokenSaver
    {
        public void SaveToken(string token);
        public string SetToken();
    }

    public class TokenSaver : ITokenSaver
    {
        private string token;

        public void SaveToken(string token)
        {
            this.token = token;
        }

        public string SetToken()
        {
            return token;
        }
    }
}

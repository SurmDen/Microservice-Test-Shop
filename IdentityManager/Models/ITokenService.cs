using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Models
{
    public interface ITokenService
    {
        public string CreateToken(RequestUser user);
        public bool ValidateToken(string token);
    }
}

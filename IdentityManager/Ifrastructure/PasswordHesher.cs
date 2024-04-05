using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace IdentityManager.Ifrastructure
{
    public class PasswordHesher
    {
        public static string HeshPassword(string password)
        {
            SHA256 hesh = SHA256.Create();
            byte[] heshedBytes = hesh.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in heshedBytes)
            {
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Models
{
    public class ResponseUser
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}

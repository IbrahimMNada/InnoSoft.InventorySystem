using InnoSoft.InventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Authentication
{
    public class AuthenticationResult
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}

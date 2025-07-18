using InnoSoft.InventorySystem.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core.Entities
{
    public class User : Entity, IAuditable
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles UserRole { get; set; }
    }
}

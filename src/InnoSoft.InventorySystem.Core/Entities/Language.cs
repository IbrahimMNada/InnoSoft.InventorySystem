using InnoSoft.InventorySystem.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core.Entities
{
    public class Language : Entity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}

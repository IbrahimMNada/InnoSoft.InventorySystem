using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DependencyScannerIgnoreAttribute : Attribute
    {
    }
}

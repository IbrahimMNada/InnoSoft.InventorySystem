using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core.Abstractions
{
    public interface ICurrentUser
    {
        public Guid UserId { get; }
        public string Username { get; }
        public Roles Role { get; }
        public List<Permissions> AllowedPermissions { get; }
    }
}

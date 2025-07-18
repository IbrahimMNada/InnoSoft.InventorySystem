using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application
{
    public class CurrentUser : ICurrentUser
    {
        public Guid UserId { get; }
        public string Username { get; }
        public Roles Role { get; }
        public List<Permissions> AllowedPermissions { get; }

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated ?? false;
             
            if (!user || httpContextAccessor == null)
                return;

            Username = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? null;
            UserId = Guid.Parse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid) ?? Guid.Empty.ToString());
            var roleClaim = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role) ?? null;
            if (!Enum.TryParse(roleClaim, out Roles parsedRole))
            {
                //  throw new Exception($"Invalid role claim: '{roleClaim}'");
            }
            AllowedPermissions = PermissionRoleMapping[parsedRole];
        }
        public static Dictionary<Roles, List<Permissions>> PermissionRoleMapping => new()
        {
            {
                Roles.Admin, new List<Permissions>
                {
                    Permissions.CreateProduct,
                    Permissions.UpdateProduct,
                    Permissions.DeleteProduct,
                    Permissions.ViewProduct,
                    Permissions.AddQuantityToProduct,
                    Permissions.RemoveQuantityFromProduct,
                    Permissions.CreateCategory,
                    Permissions.UpdateCategory,
                    Permissions.DeleteCategory,
                    Permissions.ViewCategory
                }
            },
            {
                Roles.EndUser, new List<Permissions>
                {
                    Permissions.ViewProduct,
                    Permissions.ViewCategory
                }
            }
        };

    }
}

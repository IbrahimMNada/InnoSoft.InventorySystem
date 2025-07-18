using InnoSoft.InventorySystem.Application;
using InnoSoft.InventorySystem.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InnoSoft.InventorySystem.Api.Core.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequirePermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Permissions _requiredPermission;

        public RequirePermissionAttribute(Permissions requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUser = context.HttpContext.RequestServices.GetService<CurrentUser>();
            if (currentUser == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            if (currentUser.AllowedPermissions.Any() && !currentUser.AllowedPermissions.Contains(_requiredPermission))
            {
                throw new DomainException("Permissiondenied");
            }
        }
    }
}
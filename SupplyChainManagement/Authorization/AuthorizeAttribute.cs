using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SupplyChainManagement.Models;

namespace SupplyChainManagement.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) return;

            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new RedirectResult("~/Users/SignIn");
            }
            if (user != null && _roles.Any() && !_roles.Contains(user.Role))
            {
                context.Result = new RedirectResult("~/Error/Unauthorized");

            }
        }
    }
}
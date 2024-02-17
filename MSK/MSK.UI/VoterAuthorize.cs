using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace MSK.UI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class VoterAuthorize :Attribute ,IAuthorizationFilter
    {
        public string Area { get; set; }
        public string AuthenticationScheme { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the attribute is applied to the controller or action
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Check if the user is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Redirect to login if not authenticated
                context.Result = new RedirectToActionResult("Login", "IVoting", new { area = Area }, permanent: false);
                return;
            }
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var roles = context.HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();
                if(!roles.Any(r=>r=="Voter"))
                {
                    context.Result = new RedirectToActionResult("Login", "IVoting", new { area = Area }, permanent: false);
                    return;
                }
            }

            // Check if the area matches
            if (!string.IsNullOrEmpty(Area) && !context.RouteData.Values.ContainsKey("area"))
            {
                // Redirect to login if the area is required but not present
                context.Result = new RedirectToActionResult("Login", "IVoting", new { area = Area }, permanent: false);
                return;
            }

            if (!string.IsNullOrEmpty(Area) && !string.Equals(Area, context.RouteData.Values["area"]?.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                // Redirect to login if the area does not match
                context.Result = new RedirectToActionResult("Login", "IVoting", new { area = Area }, permanent: false);
                return;
            }

            // Check if the authentication scheme matches
            if (!string.IsNullOrEmpty(AuthenticationScheme) && !string.Equals(AuthenticationScheme, context.HttpContext.User.Identity.AuthenticationType, StringComparison.OrdinalIgnoreCase))
            {
                // Redirect to login if the authentication scheme does not match
                context.Result = new RedirectToActionResult("Login", "IVoting", new { area = Area }, permanent: false);
                return;
            }
        }
    }
}

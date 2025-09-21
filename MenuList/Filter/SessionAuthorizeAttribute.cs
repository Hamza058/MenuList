using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MenuList.Filter
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>().Any();

            if (allowAnonymous)
            {
                base.OnActionExecuting(context);
                return;
            }

            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
            }

            base.OnActionExecuting(context);
        }
    }
}

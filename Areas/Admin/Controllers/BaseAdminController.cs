
using JO_MOVIES.Areas.User.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JO_MOVIES.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseAdminController : Controller
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if user is admin
            var isAdmin = AccountController.Admin;

            if (!isAdmin)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "User" });
                return;
            }

            base.OnActionExecuting(context);
        }
    }

}


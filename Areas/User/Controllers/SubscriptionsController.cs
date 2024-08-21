using JO_MOVIES.Data;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace JO_MOVIES.Areas.User.Controllers
{
    [Area("User")]
    public class SubscriptionsController : Controller
    {
        private readonly AppDbContext _Context;
        public SubscriptionsController(AppDbContext appDbContext) 
        {
            _Context = appDbContext;
        }

        public ActionResult Authenticate()
        {
            if(AccountController.currentUser == null)
            return RedirectToAction("InavlidEnter","Account");

            return View();
        }
        public IActionResult Create()
        {
            Authenticate();

            return View();
        }

        public IActionResult Edit()
        {
            Authenticate();
            return View();
        }

        public IActionResult Delete()
        {
            Authenticate();
            return View();
        }

        
        public IActionResult Index()
        {
            Authenticate();
            return View();
        }

        public IActionResult ViewSubs()
        {
            return View();
        }
    }
}

using JO_MOVIES.Areas.Admin.Controllers;
using JO_MOVIES.Data;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace JO_MOVIES.Controllers
{
    [Area("Admin")]
    public class SubscriptionsController : BaseAdminController
    {
        private readonly AppDbContext _Context;
        public SubscriptionsController(AppDbContext appDbContext) 
        {
            _Context = appDbContext;
        }


        public IActionResult Create()
        {
            

            return View();
        }

        public IActionResult Edit()
        {
            
            return View();
        }

        public IActionResult Delete()
        {
            
            return View();
        }

        
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult ViewSubs()
        {
            return View();
        }
    }
}

using JO_MOVIES.Data.service;
using JO_MOVIES.Data.ViewModels;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JO_MOVIES.Areas.User.Controllers
{
    [Area("User")]
    public class TicketsController : Controller
    {
        private readonly ITicketsService _service;

        public TicketsController(ITicketsService service)
        {
            _service = service;
        }

        public IActionResult ComingSoon()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                // var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }

        //GET: Movies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        //GET: Movies/Create
       
    }
}

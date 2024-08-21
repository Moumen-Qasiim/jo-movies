using JO_MOVIES.Data;
using JO_MOVIES.Data.service;
using JO_MOVIES.Data.ViewModels;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JO_MOVIES.Areas.User.Controllers
{
    [Area("User")]
    public class MovieController : Controller
    {
       

        private readonly IMovieService _service;
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context,IMovieService service) 
        {
           _context = context;
            _service = service;
        }









        public IActionResult ComingSoon()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync();
            return View(allMovies);
        }
        public async Task<IActionResult> AdminIndex()
        {
            var allMovies = await _service.GetAllAsync();
            return View(allMovies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower()) ).ToList();

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
       



        public async Task<IActionResult> Watch (int id)
        {
            Movie movie = await _service.GetByIdAsync(id);

            if(movie != null) 
                return View(movie);


            return NotFound();



        }



       



    }
}

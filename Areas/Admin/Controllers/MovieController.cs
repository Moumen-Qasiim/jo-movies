using JO_MOVIES.Areas.Admin.Controllers;
using JO_MOVIES.Data;
using JO_MOVIES.Data.Enum;
using JO_MOVIES.Data.service;
using JO_MOVIES.Data.ViewModels;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JO_MOVIES.Controllers
{
    [Area("Admin")]
    public class MovieController : BaseAdminController
	{
       

        private readonly IMovieService _service;
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context, IMovieService service) : base()
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
                var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

               // var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }


        public async Task<IActionResult> Filter_By_Category(MovieCategory movieCategory)
        {
            List<Movie> movies = await _context.Films.OfType<Movie>().Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor).ToListAsync();
            if (movies != null)
            {
                var Filter = new FilterMovieViewModel
                {
                    Movies = movies,
                    MovieCategory = movieCategory
                };
                return View(Filter);
            }

            return View(new List<Movie>());
        }


        //GET: Movies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        //GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

           
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
            


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

                
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }
            movie.MovieType = Data.Enum.MovieType.Movie;
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }


        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
   
                ImageURL = movieDetails.ImageURL,
                VidURL = movieDetails.VidURL,
                RelaseDate = movieDetails.RelaseDate,

                MovieCategory = movieDetails.MovieCategory,
               
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
            
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

               
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }
            
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }


		public async Task<IActionResult> Delete(int id)
		{
			var producerDetails = await _service.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var producerDetails = await _service.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}



        public async Task<IActionResult> Watch (int id)
        {
            Movie movie = await _service.GetByIdAsync(id);

            if(movie != null) 
                return View(movie);


            return NotFound();



        }



       



    }
}

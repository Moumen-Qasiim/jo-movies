using JO_MOVIES.Data.service;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JO_MOVIES.Areas.User.Controllers
{
    [Area("User")]
    public class CinemaController : Controller
    {


        private readonly ICinemasService _service;

        public CinemaController(ICinemasService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

       
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

       

    }
}

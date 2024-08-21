using JO_MOVIES.Areas.Admin.Controllers;
using JO_MOVIES.Data.service;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JO_MOVIES.Controllers
{
    [Area("Admin")]
    public class CinemaController : BaseAdminController
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



        //Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        //Get: Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

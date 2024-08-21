using JO_MOVIES.Data.service;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JO_MOVIES.Areas.User.Controllers
{
    [Area("User")]
    public class ProducerController : Controller
    {

        private readonly IProducersService _service;

        public ProducerController(IProducersService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }


        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }
    }
}


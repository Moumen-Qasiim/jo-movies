using JO_MOVIES.Data.service;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace JO_MOVIES.Areas.User.Controllers
{
    [Area("User")]
    public class ActorController : Controller
	{

		private readonly IActorService _service;

		public ActorController(IActorService service)
		{
			_service = service;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var data = await _service.GetAllAsync();
			return View(data);
		}

		
		public async Task<IActionResult> Details(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);

			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}

		
		
	}


}

using JO_MOVIES.Areas.Admin.Controllers;
using JO_MOVIES.Data.service;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace JO_MOVIES.Controllers
{
	[Area("Admin")]
	public class ActorController : BaseAdminController
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

		public async Task<IActionResult> AdminIndex()
		{
			var allActors = await _service.GetAllAsync();
			return View(allActors);
		}

		//Get: Actors/Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
		{
			if (!ModelState.IsValid)
			{
				return View(actor);
			}
			await _service.AddAsync(actor);
			return RedirectToAction(nameof(Index));
		}

		//Get: Actors/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);

			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}

		//Get: Actors/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);
			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
		{
			if (!ModelState.IsValid)
			{
				return View(actor);
			}
			await _service.UpdateAsync(id, actor);
			return RedirectToAction(nameof(Index));
		}

		//Get: Actors/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);
			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);
			if (actorDetails == null) return View("NotFound");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}


}

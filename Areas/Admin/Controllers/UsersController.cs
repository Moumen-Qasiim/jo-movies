using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JO_MOVIES.Data;
using JO_MOVIES.Models;
using JO_MOVIES.Areas.Admin.Controllers;

namespace JO_MOVIES.Controllers
{
    [Area("Admin")]
    public class UsersController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task<IActionResult> get_Admins()
        {
            var allAdmins = await _context.Users.Where(user => user.Roles == Data.Static.UserRoles.Admin).ToListAsync();
            return View(allAdmins);
        }

        public async Task <IActionResult > get_Users()
        {
            var  AllUser =  await _context.Users.Where(user => user.Roles == Data.Static.UserRoles.User).ToListAsync();
            return View(AllUser);
        }
    }
}

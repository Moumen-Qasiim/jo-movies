using Microsoft.AspNetCore.Mvc;
using JO_MOVIES.Models;
using JO_MOVIES.Data;
using Microsoft.EntityFrameworkCore;
using JO_MOVIES.Data.ViewModels;
using JO_MOVIES.Models;

using JO_MOVIES.Data.service;

namespace JO_MOVIES.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {

        private readonly AppDbContext _context;
        public readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IMovieService _service;
        public static Models.User? currentUser;
        public static bool Admin = false;

        public AccountController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            if (currentUser != null)
                Admin = currentUser.Roles == Data.Static.UserRoles.Admin;

        }

        public async Task<IActionResult> AdminIndex()
        {
            
            var alluser = await _context.Users.ToListAsync();
            return View(alluser);
        }



        public IActionResult Index()
        {
            if (currentUser != null)
                return View(currentUser);

            return RedirectToAction(nameof(Login));
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(NewUserVM newUser)
        {


            if (ModelState.IsValid)
            {
                // Check if user with the same email already exists
                var existingUser = await _context.Users.FirstOrDefaultAsync(oldUser => oldUser.Email == newUser.Email);

                if (existingUser == null)
                {
                    if (newUser.Password == newUser.ConPassowrd) // Typo: Should be ConPassword
                    {
                        var user = new JO_MOVIES.Models.User()
                        {
                            Roles = Data.Static.UserRoles.User,
                            Email = newUser.Email,
                            Password = newUser.Password,
                            Name = newUser.Name,
                            profileURL = newUser.profileURL
                        };

                        // Add user to context asynchronously
                        await _context.Users.AddAsync(user);
                        await _context.SaveChangesAsync(); // Save changes to the database

                        // Redirect to login action with login information
                        return RedirectToAction(nameof(Login), new LoginVM
                        {
                            EmailAddress = user.Email,
                            Password = String.Empty, // Assuming this is for auto-login purpose

                        });
                    }
                    else if (newUser.ConPassowrd != newUser.Password)
                    {
                        ModelState.AddModelError(string.Empty, "Passwords do not match.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email already exists. Please use a different email.");
                }
            }

            // If ModelState is not valid or any error occurs, return the SignUp view with newUser model
            return View(newUser);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM user)
        {
            if (ModelState.IsValid)
            {
                // Attempt to find the user in the database
                var authenticatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.EmailAddress && u.Password == user.Password);

                if (authenticatedUser != null)
                {
                    currentUser = authenticatedUser;
                    // Store user ID in session (example: you can use session if needed)
                    HttpContext.Session.SetString("UserId", currentUser.Id.ToString());

                    // Check if "Remember Me" is checked
                    if (user.RememberMe)
                    {
                        // Create a cookie to remember the user
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.UtcNow.AddDays(30), // Cookie expiration time
                            HttpOnly = true, // Cookie is only accessible via HTTP(S)
                            Secure = true // Use in production with HTTPS
                        };

                        _httpContextAccessor.HttpContext.Response.Cookies.Append("RememberMeCookie", currentUser.Id.ToString(), cookieOptions);
                    }
                    return RedirectToAction("Index", "Movie");
                }
                else
                {
                    // Handle invalid login
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }

                authenticatedUser = null;
            }

            // Clear the Model field to prevent browser auto-fill



            // If model state is not valid, return the login view with errors
            return View(user);
        }



        public ActionResult Logout()
        {
            currentUser = null;
            return RedirectToAction("Index", "Movie");

        }





        public IActionResult ShopingCart()
        {
            // Assuming currentUser is fetched from somewhere in your controller or injected
            if (currentUser == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Initialize a list to hold movies in the cart
            List<Ticket> movies = new ();

            // Retrieve shopping cart items for the current user
            var cartItems = _context.ShopingCartItemsDb
                                  .Where(item => item.UserId == currentUser.Id && item.Onwed == false)
                                  .ToList();

            // Loop through each cart item and fetch corresponding movie
            foreach (var cartItem in cartItems)
            {
                // Find the movie associated with the cart item
                var movie = _context.Films.OfType<Ticket>().FirstOrDefault(m => m.Id == cartItem.MovieId);

                if (movie != null)
                {
                    movies.Add(movie); // Add the movie to the list
                }
                // Handle case where movie is null if needed
            }

            return View(movies); // Pass the list of movies to the view
        }


        public ActionResult AddTicketItem(int id)
        {
            if (currentUser == null)
            {
                return RedirectToAction(nameof(Login));
            }

           
            var newItem = _context.Movies.FirstOrDefault(x => x.Id == id);

          
            if (newItem != null)
            {
                var existItem = _context.ShopingCartItemsDb.FirstOrDefault(X => X.MovieId == id && X.Email == currentUser.Email);

                if (existItem == null)
                {
                    var Item = new ShopingCartItems()
                    {
                        MovieId = id,
                        UserId = currentUser.Id,
                        Email = currentUser.Email,

                    };

                    _context.ShopingCartItemsDb.Add(Item);
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Item added successfully" });
                }
                else
                {
                    if(existItem.Onwed == true)
                    return Json(new { success = false, message = "Item already Owned" });
                    else
                        return Json(new { success = false, message = "Item already exists in the cart" });
                }
            }

            return Json(new { success = false, message = "Movie not found" });
        }
    
    
        public ActionResult DeleteTicketItem(int id)
        {
            if (currentUser == null)
            {
                return RedirectToAction(nameof(Login));
            }
            // Find the item in the shopping cart
            var cartItem = _context.ShopingCartItemsDb
                                .FirstOrDefault(item => item.UserId == currentUser.Id && item.MovieId == id);

            if (cartItem != null)
            {
                // Remove the item from the database context
                _context.ShopingCartItemsDb.Remove(cartItem);
                _context.SaveChanges();  // Save changes to the database
            }
           
                return Json(new { success = true });
  
        }

        public IActionResult Payment()
        {
            if(currentUser == null)
                return RedirectToAction(nameof(Login));
            return View();
        }
        [HttpPost]
        public IActionResult Payment(CreditCard creditCard)
        {
            if (currentUser == null)
                return RedirectToAction(nameof(Login));

            if (ModelState.IsValid)
            {

                    
                    _context.CreditCards.Add(creditCard);
                JO_MOVIES.Models.User? user = _context.Users.FirstOrDefault(x => x.Id == currentUser.Id);
                    
                    _context.SaveChanges();
                    return RedirectToAction(nameof(ShopingCart));
                
            }
            return View(creditCard);
        }

        public ActionResult AddOwnedItem()
        {
            if (currentUser == null)
                return RedirectToAction(nameof(Login));

            
            List<ShopingCartItems> BoughtItems = _context.ShopingCartItemsDb.Where(Item => Item.UserId == currentUser.Id && Item .Onwed == false).ToList();

            foreach (var item in BoughtItems)
            {
                item.Onwed = true;
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Sucess));
        }

        public IActionResult InvalidEnter()
        {
            return View();
        }


        public IActionResult Sucess()
        {
            return View();
        }

    }
}

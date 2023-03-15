using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using TicketSaler.Models;

namespace TicketSaler.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;
     
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {

            return View(await _context.Events.ToListAsync());
        }

        [Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Denied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            var form = HttpContext.Request.Form;
            if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                return Redirect("Login");
            string login = form["email"];
            string password = form["password"];

            User? person = _context.Users.FirstOrDefault(p => p.Email == login && p.Password == password);
            if (person is null) return Unauthorized();
           

            await HttpContext.SignInAsync(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new List<Claim>
                                        {
                                        new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserId.ToString()),
                                        new Claim(ClaimsIdentity.DefaultRoleClaimType,person.AcsessLevel )
                                        },
                        "Cookies")));
            return Redirect(returnUrl ?? "/");
        }

        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Login");
        }
    }
}
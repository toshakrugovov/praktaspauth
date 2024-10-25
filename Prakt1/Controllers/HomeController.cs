using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prakt1.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Prakt1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbcontext _appDbcontext;

        public HomeController(AppDbcontext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _appDbcontext.users.ToListAsync());
        }

        public IActionResult SignIn()
        {
            if (HttpContext.Session.Keys.Contains("AuthUser"))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
       
            if (ModelState.IsValid)
            {
                
                var user = await _appDbcontext.users
                    .FirstOrDefaultAsync(u => u.email == model.Login && u.password == model.Password);

                if (user != null)
                {
                  
                    HttpContext.Session.SetString("AuthUser", user.email);
                    HttpContext.Session.SetString("UserRole", user.role_id.ToString());

                    
                    await Authenticate(user.email);

                   
                    return RedirectToAction("Index", "Home");
                }

               
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, userName) };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("AuthUser");
            return RedirectToAction("SignIn");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(users person)
        {
            if (ModelState.IsValid)
            {
              
                if (await _appDbcontext.users.AnyAsync(u => u.email == person.email))
                {
                    ModelState.AddModelError("", "Пользователь с таким email уже существует.");
                    return View(person);
                }

          
                await _appDbcontext.users.AddAsync(person);
                await _appDbcontext.SaveChangesAsync();
                return RedirectToAction("SignIn");
            }
            return View(person);
        }
        public async Task<IActionResult> Catalog()
        {
            var products = await _appDbcontext.Products.ToListAsync();
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> Profile()
        {
           
            var email = HttpContext.Session.GetString("AuthUser");
            if (email == null)
            {
                return RedirectToAction("SignIn");
            }

          
            var user = await _appDbcontext.users.FirstOrDefaultAsync(u => u.email == email);

            if (user == null)
            {
                return NotFound(); 
            }
            

       
            var role = await _appDbcontext.roles.FirstOrDefaultAsync(r => r.id == user.role_id);

            
            if (role.role_name == "Администратор")
            {
                ViewBag.Name = user.name;
                ViewBag.UserRole = "Администратор";
                return View("Administrator");
            }
            else if (role.role_name == "Менеджер")
            {
                ViewBag.Name = user.name;
                ViewBag.UserRole = "Менеджер";
                return View("Manager");
            }
            else
            {
                ViewBag.Name = user.name;
                ViewBag.UserRole = "Покупатель";
                return View("Profile");
            }
        }

    }
}

using ASPNETCore_HomeTasks_11.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ASPNETCore_HomeTasks_11.Services;

namespace ASPNETCore_HomeTasks_11.Controllers
{
    public class AccountController : Controller
    {
        UsersMessagesContext usersMessagesContext;

       public AccountController(UsersMessagesContext context)
       {
            usersMessagesContext = context;
       }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await usersMessagesContext.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == PasswordHasherService.HashPassword(model.Password));
                if (user != null)
                {
                    await Authenticate(model.Login);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await usersMessagesContext.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    usersMessagesContext.Users.Add(
                        new User {Id = Guid.NewGuid(), 
                            Login = model.Login, 
                            Name = model.Name, 
                            Password = PasswordHasherService.HashPassword(model.Password)});

                    await usersMessagesContext.SaveChangesAsync();

                    await Authenticate(model.Login); 

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Market.Abstract.Service;
using Market.Models.DTO;
using Market.MVC.Models;

namespace Market.MVC.Controllers
{
    /// <summary>
    /// Контроллер для управления аккаунтами
    /// </summary>
    public class AccountController : BaseController
    {
        public AccountController(IMarketService ctx) : base(ctx) { }

        /// <summary>
        /// Page for authorize user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Authorize user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = service.AccountManager.FindByLoginPassword(model.Login, model.Password);
                if (userDTO != null)
                {
                    await Authenticate(model.Login); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login and/or password");
            }
            return View(model);
        }

        /// <summary>
        /// Page for registration a new user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = new UserDTO
                {
                    Login = model.Login,
                    Password = model.Password
                };

                //Add user
                if (service.AccountManager.Register(userDTO)) //true - user successfully added
                {
                    await Authenticate(model.Login);

                    return RedirectToAction("Index", "Home");
                }
                else //if hte same user is already created
                    ModelState.AddModelError("", "Same user is already created. Input another login");
            }
            return View(model);
        }

        /// <summary>
        /// Выход пользвоателя
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// For test authorization
        /// </summary>
        /// <returns></returns>
        // GET: Account
        public IActionResult Index()
        {
            return Content(User.Identity.Name);
        }

        private async Task Authenticate(string userName)
        {
            // create one claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // create object ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // set authorize cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}
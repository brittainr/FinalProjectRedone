using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectRedone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectRedone.Controllers
{
    public class AccountController : Controller
    {


        private UserManager<UserModel> userManager;
        private SignInManager<UserModel> signInManager;

        public AccountController(UserManager<UserModel> usrMngr, SignInManager<UserModel> signInMngr)
        {
            userManager = usrMngr;
            signInManager = signInMngr;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel { UserName = model.Username, Name = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LogInVM
            {
                ReturnUrl = returnURL
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }

        public ActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LogOff()
        {


            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

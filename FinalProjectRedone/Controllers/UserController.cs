using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectRedone.Models;
using FinalProjectRedone.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectRedone.Controllers
{
    //[Authorize(Roles = "Admin")]
   
    public class UserController : Controller
    {
        
    //page 691
    // ignore when youa dd authorization to admin page ignore area admin we do not have one called area. 

   

    
        private UserManager<UserModel> userManager;
        private RoleManager<IdentityRole> roleManager;
        IFinance repo;
        public UserController(UserManager<UserModel> userMngr, RoleManager<IdentityRole> roleMngr, IFinance r)
        {
            userManager = userMngr;
            roleManager = roleMngr;
            repo = r;

        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Admin()
        {
            List<UserModel> users = new List<UserModel>();
            foreach (UserModel user in userManager.Users)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }
            UserVM model = new UserVM
            {
                Users = users,
                Roles = roleManager.Roles
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            UserModel user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                //check if they have stories
                //StoriesContext context = new StoriesContext(new Microsoft.EntityFrameworkCore.DbContextOptions<StoriesContext>());
                //var stories = context.Story.Where(s => s.User.Id == id).ToList();
                // context.Story.RemoveRange(stories);

                //context.SaveChanges();
                //var deleteList = repo.Forum.Where(p => p.User.Id == id).ToList();


                IdentityResult result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";

                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Admin");
        }
        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            var roleExists = await roleManager.RoleExistsAsync("Admin");

            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. " + "Click 'Create Admin Role' button to create it";
            }
            else
            {

                UserModel user = await userManager.FindByIdAsync(id);
                var result = await userManager.AddToRoleAsync(user, "Admin");
                if (result.Succeeded)
                {
                    return RedirectToAction("Admin");
                }
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            UserModel user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByNameAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Admin");
        }


        [HttpPost]
        public async Task<IActionResult> DeletePost(UserModel model)
        {
            repo.DeleteRange(model.Id);
            return RedirectToAction("Admin");
        }


    }
}

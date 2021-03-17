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
    public class ForumController:Controller
    {

        IFinance repo;
        UserManager<UserModel> userManager;


        public ForumController(IFinance r, UserManager<UserModel> u)
        {
            //object passed in and assigning object to context. 
            repo = r;
            userManager = u;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forum(string date, string name)
        {

            List<Post> posts = new List<Post>();

            var testAll = repo.Forum.ToList();

            if (!String.IsNullOrEmpty(name))
            {
                posts = (from r in repo.Forum
                         where r.User.UserName == name
                         select r).ToList();

            }
            else if (!String.IsNullOrEmpty(date))
            {
                var parseDate = DateTime.Parse(date);
                posts = (from r in repo.Forum
                         where r.Date.Date == parseDate.Date
                         select r).ToList();
            }
            return View(posts);


        }
        public IActionResult Post()
        {
            Post model = new Post();//new object story created.
            UserModel userName = new UserModel();//new object created
            userName.Name = "Test";
            model.User = userName;
            //you need to make the username from the model equal the initialized model.

            return View(model); //put model into view.


        }

        [HttpPost]
        public IActionResult Post(Post model)
        {

            // model.User = new User();
            if (ModelState.IsValid)
            {
                model.User = repo.GetUser(User.Identity.Name);
                if (model.User != null)
                {
                    repo.AddPost(model);
                }

                return Redirect("Forum");// pass to story
            }
            else
            {
                //how to add this back 
                //AddErrors();
                return View(model);
            }
        }
        [Authorize]
        public IActionResult Reply(int postId)
        {
            var replyVM = new ReplyVM { PostID = postId };
            return View(replyVM);
        }

        [HttpPost]
        public RedirectToActionResult Reply(ReplyVM replyVM)
        {
            // Comment is the domain model
            var reply = new Reply { ReplyText = replyVM.ReplyText };
            reply.Replier = userManager.GetUserAsync(User).Result;
            reply.ReplyDate = DateTime.Now;

            // Retrieve the review that this comment is for
            var post = (from r in repo.Forum
                        where r.PostId == replyVM.PostID
                        select r).First<Post>();

            // Store the review with the comment in the database
            post.Replies.Add(reply);
            repo.UpdatePost(post);

            return RedirectToAction("Forum");
        }
        private void AddErrors()
        {
            ModelState.AddModelError("", "Error has occured.");
        }
        [HttpGet]
        public IActionResult Quiz()
        {
            return View();
        }
    }
}

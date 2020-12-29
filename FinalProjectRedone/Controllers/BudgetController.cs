using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectRedone.Repos;
using Microsoft.AspNetCore.Mvc;
using FinalProjectRedone.Models;

namespace FinalProjectRedone.Controllers
{
    public class BudgetController : Controller

    {

        IFinance repo;



        //initialize
        public BudgetController(IFinance r)
        {
            repo = r;//object passed in and assigning object to context. 

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Budget()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Budgets(BudgetViewModel model, string Username, string submitButton)//pass in QuizModel as a quizmodel then instantiate it and add it. 
        {
            List<TaxModel> finances = new List<TaxModel>();
            BudgetModel newItem = new BudgetModel();
            List<BudgetModel> budgetItems = new List<BudgetModel>();

            if(submitButton == "Search")
            {
                if (!String.IsNullOrEmpty(Username))
                {
                    finances = repo.Finances.Where(f => f.User.Name == Username).ToList();
                }
                newItem.User = new UserModel() { Name = Username };
                budgetItems = repo.Budget.ToList<BudgetModel>().Where(b => b.User.Name == Username).ToList();
            }
            else
            {
                if (repo.CheckForBudget(model.NewBudgetItem.User.Name))
                {
                    repo.AddBudgetItem(model.NewBudgetItem);
                    ViewBag.Saved = "This budget record has been added to the database.";
                    budgetItems = repo.Budget.ToList<BudgetModel>().Where(b => b.User.Name == model.NewBudgetItem.User.Name).ToList();
                    finances = repo.Finances.Where(f => f.User.Name == model.NewBudgetItem.User.Name).ToList();
                }
                else
                {
                    ViewBag.Error = "The username you have provided does not have a salary on record.";
                }
            }


            return View(new BudgetViewModel() { Finances = finances, NewBudgetItem = newItem, BudgetItems = budgetItems }); ;
            // machine gun all the 
            //  return View(model);
            //repo.AddPost(model)^


        }

        public IActionResult DeleteBudgetItem(int id)
        {

            repo.DeleteBudgetItem(id);
            
           //use a linq statement to delete from the database. then call it in the view file on the delete element. 
            return RedirectToAction("Budgets"); 
            //return Redirect()
        }

        public IActionResult Budgets()
        {
            var budgetitems = new List<BudgetModel>();
            List<TaxModel> finances = new List<TaxModel>();

            return View(new BudgetViewModel() { Finances = finances, NewBudgetItem = new BudgetModel(), BudgetItems = budgetitems });
        }

        
    }
}

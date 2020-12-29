using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectRedone.Models;
using FinalProjectRedone.Repos;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectRedone.Controllers
{
    public class TaxController : Controller
    {
        IFinance repo;

        public TaxController(IFinance r)
        {
            repo = r;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Tax()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Tax(TaxModel model)
        {
            ViewBag.Success = false;



            if(!String.IsNullOrEmpty(model.MonthlyQuestion) && model.MonthlyQuestion.Substring(0).ToLower() == "y")
            {
                model.Medicare = Math.Floor(model.MonthlyIncome * 0.0145);
                model.SocialSecurity = Math.Floor(model.MonthlyIncome * 0.062);
                repo.AddMonth(model);
                ViewBag.Success = true;
                ViewBag.Saved = "Your finances have been calculated and saved successfully.";
            }
            else
            {
                ViewBag.Error = "You have not indicated that this is a monthly income. No finance infomration has been saved.";
            }
            

            return View(model);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class BudgetModel
    {
        [Key]
        public int BudgetID { get; set; }
        public string BudgetItem { get; set; }
        public double Amount { get; set; }
        public UserModel User { get; set; }

        //each budget item will be attatched to a user. 
        //then on tax model we will be able to create a list of budget models. for a user ! 
        //once we build i fanance and finance repos then do migrations. 
        //this helps you migrate to the database. 
        //get stuff to save get all database working then we can meet next week . 
    }

    public class BudgetViewModel
    {
        public List<TaxModel> Finances { get; set; }
        public BudgetModel NewBudgetItem { get; set; }
        public List<BudgetModel> BudgetItems { get; set; }
        public double Limit { get; set; }
    }
}

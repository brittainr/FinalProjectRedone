using FinalProjectRedone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Repos
{
    public class FakeFinanceRepo : IFinance
    {
        List<TaxModel> finance = new List<TaxModel>();

        List<BudgetModel> budget = new List<BudgetModel>();
       


        public IQueryable<TaxModel> Finances { get { return finance.AsQueryable<TaxModel>(); } }

        public IQueryable<BudgetModel> Budget { get { return budget.AsQueryable<BudgetModel>(); } }

        public void AddMonth(TaxModel finances)
        {
            finances.TaxID = finance.Count;
            finance.Add(finances);
        }

        public TaxModel GetFinanceByName(string name)
        {
            var bracket = finance.Find(f => f.User.Name == name);
            return bracket;
        }

        public bool CheckForBudget(string username)
        {
            var result = finance.Find(f => f.User.Name == username);
            return result == null ? false : true;
        }




        public void AddBudgetItem(BudgetModel model)
        {

            budget.Add(model);
        }

        public void DeleteBudgetItem(int id)
        {
           budget.Remove(budget.Find(b => b.BudgetID==id));

        }





    }
}

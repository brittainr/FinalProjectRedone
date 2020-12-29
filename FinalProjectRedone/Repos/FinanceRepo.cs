using FinalProjectRedone.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Repos
{
    public class FinanceRepo : IFinance
    {
        private TaxContext context;
        List<TaxModel> finance = new List<TaxModel>();

        List<BudgetModel> budget = new List<BudgetModel>();



        public FinanceRepo(TaxContext c)
        {
            context = c;
        }
        public IQueryable<TaxModel> Finances
        {
            get
            {
                return context.Finances.Include(Finances => Finances.User);
            }
        }


        public IQueryable<BudgetModel> Budget 
        {
            get
            {
                return context.Budget.Include(budget => budget.User);
            }
        }



        public void AddMonth(TaxModel finance)
        {
            var existingRecord = context.Finances.Where(f => f.User.Name == finance.User.Name && f.Month.ToLower() == finance.Month.ToLower()).FirstOrDefault();
            if(existingRecord == null)
            {
                context.Finances.Add(finance);
                context.SaveChanges();
            }
            else
            {
                existingRecord.MonthlyIncome = finance.MonthlyIncome;
                existingRecord.Medicare = finance.Medicare;
                existingRecord.SocialSecurity = finance.SocialSecurity;
                context.SaveChanges();
            }

        }

      

        public TaxModel GetFinanceByName(string name)
        {
            var finance = context.Finances.Find(name);
            return finance;
        }

        public bool CheckForBudget(string username)
        {
            var finance = context.Finances.Where(f => f.User.Name == username).FirstOrDefault();
            return finance == null ? false : true;
        }

        public void AddBudgetItem(BudgetModel model)
        {
            context.Budget.Add(model);

            var finance = context.Finances.Include(f => f.Expenses).Where(f => f.User.Name == model.User.Name).FirstOrDefault();
            if (finance.Expenses == null) { finance.Expenses = new List<BudgetModel>(); }
            finance.Expenses.Add(model);

            context.SaveChanges();
        }
        public void DeleteBudgetItem(int id)
        {

            var rec = context.Budget.Find(id);
            context.Budget.Remove(rec);
            context.SaveChanges();
            
            
        }
    
    }
}

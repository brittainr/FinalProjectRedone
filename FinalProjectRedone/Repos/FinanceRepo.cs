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
        public IQueryable<Post> Forum
        {
            get

            {
                // Get all the Review objects in the Reviews DbSet
                // and include the Reivewer object and list of comments in each Review.
                return context.Posts.Include(post => post.User)
                         .Include(post => post.Replies)
                         .ThenInclude(reply => reply.Replier);
            }
        }


        public void AddPost(Post post)
        {
            post.User = (UserModel)context.Users.Where(u => u.UserName == post.User.Name).FirstOrDefault();// allowing the story object to be virtual 
            //maked this possible. assigning full connected object store.user.name which is an asp net user object. first or default is a search. 
            //it will return first object or if it is empty it will return null.
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void DeleteRange(string id)
        {
            var posts = context.Posts.Where(s => s.User.Id == id).ToList();
            context.Posts.RemoveRange(posts);
            context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            post.User = (UserModel)context.Users.Where(u => u.UserName == post.User.Name).FirstOrDefault();

            context.Posts.Remove(post);
            context.SaveChanges();
        }

        public Post GetPostByTitle(string Title)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUser(string username)
        {
            var user = (UserModel)context.Users.Where(p => p.UserName == username).FirstOrDefault();
            return user;
        }

        public void UpdatePost(Post post)
        {
            context.Posts.Update(post);   // Find the review by ReviewID and update it
            context.SaveChanges();
        }

        //public void UpdateCold(string id, string cold)
        //{
        //    UserModel query = context.Users.Where(u => u.Id == id).FirstOrDefault();

        //    query.HowCold = cold;
        //    context.SaveChanges();

        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectRedone.Models;

namespace FinalProjectRedone.Repos
{
    public interface IFinance
    {

        IQueryable<BudgetModel> Budget { get; }
        IQueryable<TaxModel> Finances { get; }
        void AddMonth(TaxModel finance);
        void AddBudgetItem(BudgetModel model);
        void DeleteBudgetItem(int id);
        bool CheckForBudget(string username);
       
            
        TaxModel GetFinanceByName(string name);

        //weather 
        IQueryable<Post> Forum { get; }

        void AddPost(Post post);
        void DeleteRange(string id);
        void DeletePost(Post post);
        Post GetPostByTitle(string Title);
        void UpdatePost(Post post);
        UserModel GetUser(string username);
        //void UpdateCold(string id, string cold);
    }
}

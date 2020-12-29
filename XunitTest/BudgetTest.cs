using System;
using System.Collections.Generic;
using System.Text;
using FinalProjectRedone.Models;
using FinalProjectRedone.Controllers;
using FinalProjectRedone.Repos;
using Xunit;
using System.Linq;
namespace XunitTest
{
    public class BudgetTest
    {
        [Fact]
        public void Budgets()
        {
            var fakerepo = new FakeFinanceRepo();
            var controller2 = new BudgetController(fakerepo);

            var user = new UserModel()
            {
                Name = "mindy",
            };
            var taxes = new TaxModel()
            {
                User = user,
                Month = "December",
                MonthlyIncome = 4000.00,
                MonthlyQuestion = "y",
            };

            //Act
            fakerepo.AddMonth(taxes);
            var user2 = new UserModel()
            {
                Name = "Lydia",
            };
            var taxes2 = new TaxModel()
            {
                User = user2,
                Month = "March",
                MonthlyIncome = 2000.00,
                MonthlyQuestion = "y",
            };
            fakerepo.AddMonth(taxes2);
            //future tests consider using arrays instead of typing everythong out like this. it will save time !
            var budgetitem = new BudgetModel()
            {
                BudgetItem = "Bts albums",
                Amount = 1000.00,
                User = user,

            };
            //compounded object, must reference via this method. creating object within the object and setting its attribute from there.
            controller2.Budgets(new BudgetViewModel() { NewBudgetItem = budgetitem }, "", "submit");

            var fetchPost = fakerepo.Budget.Where(b => b.BudgetID == budgetitem.BudgetID).FirstOrDefault();


            //Ensure that the review was added to the repository
            Assert.True(budgetitem.User.Name == fetchPost.User.Name);
            Assert.True(budgetitem.Amount == fetchPost.Amount);
            Assert.True(budgetitem.BudgetItem == fetchPost.BudgetItem);

        }
    }
}

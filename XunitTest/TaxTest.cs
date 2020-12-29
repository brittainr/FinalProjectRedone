using System;
using Xunit;
using FinalProjectRedone.Models;
using FinalProjectRedone.Controllers;
using FinalProjectRedone.Repos;


namespace XunitTest
{
    public class TaxTest
    {
        [Fact]
        public void Taxes()
        {
            var fakerepo = new FakeFinanceRepo();
            var controller = new TaxController(fakerepo);

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
            controller.Tax(taxes);
            var user2 = new UserModel()
            {
                Name = "Lydia",
            };
            var taxes2 = new TaxModel()
            {
                User =user2,
                Month = "March",
                MonthlyIncome = 2000.00,
                MonthlyQuestion = "y",
            };



            //Act
            controller.Tax(taxes2);

                /*select * from Posts where title= "Love this site"*/
                //var query = from e in fakerepo.Posts
                //  where e.Title == "Love this site"
                // select e; this is how you access data using linq from a  database.

                var fetchPost = fakerepo.GetFinanceByName("mindy");

            //Ensure that the review was added to the repository
                Assert.True(taxes.User.Name == fetchPost.User.Name);
                Assert.True(taxes.Month == fetchPost.Month);
                Assert.True(taxes.MonthlyIncome == fetchPost.MonthlyIncome);
                Assert.True(taxes.MonthlyQuestion == fetchPost.MonthlyQuestion);


        }
        }
    }


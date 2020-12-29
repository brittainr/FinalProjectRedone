using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class TaxModel
    {
        [Key]
        public int TaxID { get;  set; }
        public UserModel User { get; set; }

        public string Month { get; set; }
      
        public double Medicare { get; set; }
        //multiply by 0.0145
        public double SocialSecurity { get; set; }
        //multiply by  0.062;
        public int TaxedIncome { get; set; }
        public double MonthlyIncome { get; set; }

        public List<BudgetModel> Expenses { get; set; }

        [NotMapped]
        public string MonthlyQuestion { get; set; }



       
        // Sorry have no entered a monthlhy salaray :( this  wont work for you.


        //user needs to be able to enter all stuff and view it. so after submit . it will display the username 
        // and say for each line that does calculations ie 
        //medicare and social security, it will say "name : user name"  
        //"monthly amount entered: user amount entered" 
        // social security amount: 
        // medicare amount: 
        //Total Amount: 
     


    }
}

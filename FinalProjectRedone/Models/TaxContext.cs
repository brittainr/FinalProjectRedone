using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class TaxContext : DbContext
    {

        public TaxContext( DbContextOptions<TaxContext> options) : base(options)
        {

        }

      


        public DbSet<BudgetModel> Budget { get; set; }
        public DbSet<TaxModel> Finances { get; set; }
        public DbSet<UserModel> Users { get; set; }
     



    }

}

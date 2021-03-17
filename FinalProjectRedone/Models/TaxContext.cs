using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRedone.Models
{
    public class TaxContext : IdentityDbContext<UserModel>
    {

        public TaxContext( DbContextOptions<TaxContext> options) : base(options)
        {

        }

      


        public DbSet<BudgetModel> Budget { get; set; }
        public DbSet<TaxModel> Finances { get; set; }
        // public DbSet<UserModel> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        //public DbSet<AppUser>  User  { get; set; }
        public DbSet<Reply> Replies { get; set; }


    }

}

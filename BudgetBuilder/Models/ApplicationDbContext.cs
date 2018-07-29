using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BudgetBuilder.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //public System.Data.Entity.DbSet<ApplicationUser> ApplicationUser { get; set; }
        public System.Data.Entity.DbSet<BudgetBuilder.Models.Building> Buildings { get; set; }
        public System.Data.Entity.DbSet<BudgetBuilder.Models.Trade> Trades { get; set; }
    }
}
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BudgetBuilder.Models
{
    
    public class ApplicationUser : IdentityUser
    {
        // Table properties
        // TODO - data annotations
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Collection of Child BuildingModels
        public ICollection<BuildingModels> Buildings { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>().Property(p => p.Id).HasColumnName("ApplicationUserId");
        }

        //public System.Data.Entity.DbSet<ApplicationUser> ApplicationUser { get; set; }
        public System.Data.Entity.DbSet<BudgetBuilder.Models.BuildingModels> BuildingModels { get; set; }
        public System.Data.Entity.DbSet<BudgetBuilder.Models.TradeModels> TradeModels { get; set; }
    }
}
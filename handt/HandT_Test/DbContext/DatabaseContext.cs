using HandT_Test_PG.Authentication;
using HandT_Test_PG.DomainEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HandT_Test.DbContext
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        //public DbSet<Events> Events { get; set; }
        //public DbSet<Categories> Categories { get; set; }
        //public DbSet<Feedbacks> Feedbacks { get; set; }
        //public DbSet<Payments> Payments { get; set; }
        //public DbSet<Registrations> Registrations { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

using System.Data.Entity;
using HospitalManager.DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalManager.DAL.EF
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            base.OnModelCreating(modelBuilder);
        }
    }
}

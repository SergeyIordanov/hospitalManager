using System.Data.Entity;
using HospitalManager.DAL.Entities;
using HospitalManager.DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalManager.DAL.EF
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            Database.SetInitializer(new StoreDbInitializer());
        }

        public DatabaseContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Temp> Temps { get; set; }

        public DbSet<Example> Examples { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            base.OnModelCreating(modelBuilder);
        }
    }
}

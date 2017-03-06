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
            Database.SetInitializer(new TempInitializer());
        }

        public DatabaseContext()
        {
        }

        public DatabaseContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<ClientProfile>()
                .HasMany(profile => profile.Payments)
                .WithRequired(payment => payment.ClientProfile)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
    public class TempInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            db.SaveChanges();
        }
    }
}
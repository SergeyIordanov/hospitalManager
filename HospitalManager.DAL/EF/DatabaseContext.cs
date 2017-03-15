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

        public DbSet<Artifact> Artifacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);

            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.Roles).WithRequired().HasForeignKey(p => p.UserId);
            modelBuilder.Entity<ApplicationRole>().HasMany(p => p.Users).WithRequired().HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId });

            modelBuilder.Entity<ClientProfile>()
                .HasMany(profile => profile.Payments)
                .WithRequired(payment => payment.ClientProfile)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ClientProfile>()
                .HasMany(profile => profile.Artifacts)
                .WithRequired(artifact => artifact.ClientProfile)
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
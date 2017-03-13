namespace HospitalManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnPluralTableNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ClientProfile", newName: "ClientProfiles");
            RenameTable(name: "dbo.ApplicationUser", newName: "ApplicationUsers");
            RenameTable(name: "dbo.IdentityUserClaim", newName: "IdentityUserClaims");
            RenameTable(name: "dbo.IdentityUserLogin", newName: "IdentityUserLogins");
            RenameTable(name: "dbo.IdentityUserRole", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.Payment", newName: "Payments");
            RenameTable(name: "dbo.IdentityRole", newName: "IdentityRoles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.IdentityRoles", newName: "IdentityRole");
            RenameTable(name: "dbo.Payments", newName: "Payment");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "IdentityUserRole");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "IdentityUserLogin");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "IdentityUserClaim");
            RenameTable(name: "dbo.ApplicationUsers", newName: "ApplicationUser");
            RenameTable(name: "dbo.ClientProfiles", newName: "ClientProfile");
        }
    }
}

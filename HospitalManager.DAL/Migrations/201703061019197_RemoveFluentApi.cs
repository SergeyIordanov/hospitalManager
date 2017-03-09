namespace HospitalManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFluentApi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "ClientProfile_Id", "dbo.ClientProfiles");
            DropIndex("dbo.Payments", new[] { "ClientProfile_Id" });
            AlterColumn("dbo.Payments", "ClientProfile_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Payments", "ClientProfile_Id");
            AddForeignKey("dbo.Payments", "ClientProfile_Id", "dbo.ClientProfiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "ClientProfile_Id", "dbo.ClientProfiles");
            DropIndex("dbo.Payments", new[] { "ClientProfile_Id" });
            AlterColumn("dbo.Payments", "ClientProfile_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Payments", "ClientProfile_Id");
            AddForeignKey("dbo.Payments", "ClientProfile_Id", "dbo.ClientProfiles", "Id", cascadeDelete: true);
        }
    }
}

namespace HospitalManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentForeignKeyClientProfile : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Payments", name: "ClientProfile_Id", newName: "ClientProfileId");
            RenameIndex(table: "dbo.Payments", name: "IX_ClientProfile_Id", newName: "IX_ClientProfileId");
            AlterColumn("dbo.Payments", "Signature", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "Signature", c => c.String());
            RenameIndex(table: "dbo.Payments", name: "IX_ClientProfileId", newName: "IX_ClientProfile_Id");
            RenameColumn(table: "dbo.Payments", name: "ClientProfileId", newName: "ClientProfile_Id");
        }
    }
}

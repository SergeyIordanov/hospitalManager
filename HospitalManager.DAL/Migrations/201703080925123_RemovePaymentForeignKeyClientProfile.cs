namespace HospitalManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePaymentForeignKeyClientProfile : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Payments", name: "ClientProfileId", newName: "ClientProfile_Id");
            RenameIndex(table: "dbo.Payments", name: "IX_ClientProfileId", newName: "IX_ClientProfile_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Payments", name: "IX_ClientProfile_Id", newName: "IX_ClientProfileId");
            RenameColumn(table: "dbo.Payments", name: "ClientProfile_Id", newName: "ClientProfileId");
        }
    }
}

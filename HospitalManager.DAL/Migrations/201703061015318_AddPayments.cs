namespace HospitalManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Signature = c.String(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        Details = c.String(),
                        Status = c.Int(nullable: false),
                        ClientProfile_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientProfiles", t => t.ClientProfile_Id, cascadeDelete: true)
                .Index(t => t.ClientProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "ClientProfile_Id", "dbo.ClientProfiles");
            DropIndex("dbo.Payments", new[] { "ClientProfile_Id" });
            DropTable("dbo.Payments");
        }
    }
}

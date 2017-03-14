namespace HospitalManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedartifacts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artifacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.Binary(nullable: false),
                        Description = c.String(nullable: false),
                        ClientProfile_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientProfiles", t => t.ClientProfile_Id, cascadeDelete: true)
                .Index(t => t.ClientProfile_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artifacts", "ClientProfile_Id", "dbo.ClientProfiles");
            DropIndex("dbo.Artifacts", new[] { "ClientProfile_Id" });
            DropTable("dbo.Artifacts");
        }
    }
}

namespace HospitalManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addextensionforartifacts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artifacts", "Extension", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artifacts", "Extension");
        }
    }
}

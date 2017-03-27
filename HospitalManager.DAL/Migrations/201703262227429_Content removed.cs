namespace HospitalManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contentremoved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artifacts", "Path", c => c.String(nullable: false));
            DropColumn("dbo.Artifacts", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artifacts", "Content", c => c.Binary(nullable: false));
            DropColumn("dbo.Artifacts", "Path");
        }
    }
}

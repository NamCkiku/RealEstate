namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixtableRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Room", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Room", "IsActive");
        }
    }
}

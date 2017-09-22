namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecolumTableUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AppUsers", "UpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "UpdatedDate");
            DropColumn("dbo.AppUsers", "CreatedDate");
        }
    }
}

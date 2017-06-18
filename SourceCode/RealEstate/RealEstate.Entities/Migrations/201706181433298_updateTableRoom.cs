namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTableRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Room", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Room", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Room", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Room", "UpdatedBy", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Room", "UpdatedBy");
            DropColumn("dbo.Room", "UpdatedDate");
            DropColumn("dbo.Room", "CreatedBy");
            DropColumn("dbo.Room", "CreatedDate");
        }
    }
}

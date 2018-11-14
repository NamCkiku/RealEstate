namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixtableSetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemSetting", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.SystemSetting", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.SystemSetting", "CreatedDate", c => c.DateTime());
            DropColumn("dbo.SystemSetting", "CreatedByUser");
            DropColumn("dbo.SystemSetting", "UpdatedByUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SystemSetting", "UpdatedByUser", c => c.Int());
            AddColumn("dbo.SystemSetting", "CreatedByUser", c => c.Int());
            AlterColumn("dbo.SystemSetting", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.SystemSetting", "UpdatedBy");
            DropColumn("dbo.SystemSetting", "CreatedBy");
        }
    }
}

namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablenew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemSetting",
                c => new
                    {
                        Field = c.String(nullable: false, maxLength: 250),
                        Value = c.String(maxLength: 250),
                        ValDefault = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedByUser = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedByUser = c.Int(),
                    })
                .PrimaryKey(t => t.Field);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemSetting");
        }
    }
}

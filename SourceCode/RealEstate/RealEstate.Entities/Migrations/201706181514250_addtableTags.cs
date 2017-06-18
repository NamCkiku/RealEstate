namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomTags",
                c => new
                    {
                        RoomID = c.Int(nullable: false),
                        TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.RoomID, t.TagID })
                .ForeignKey("dbo.Room", t => t.RoomID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.RoomID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.RoomTags", "RoomID", "dbo.Room");
            DropIndex("dbo.RoomTags", new[] { "TagID" });
            DropIndex("dbo.RoomTags", new[] { "RoomID" });
            DropTable("dbo.Tags");
            DropTable("dbo.RoomTags");
        }
    }
}

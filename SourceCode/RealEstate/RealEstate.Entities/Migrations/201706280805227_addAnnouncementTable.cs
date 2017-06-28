namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAnnouncementTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Content = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AnnouncementUsers",
                c => new
                    {
                        AnnouncementId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        HasRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnnouncementId, t.UserId })
                .ForeignKey("dbo.Announcements", t => t.AnnouncementId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.AnnouncementId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Announcements", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.AnnouncementUsers", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.AnnouncementUsers", "AnnouncementId", "dbo.Announcements");
            DropIndex("dbo.AnnouncementUsers", new[] { "UserId" });
            DropIndex("dbo.AnnouncementUsers", new[] { "AnnouncementId" });
            DropIndex("dbo.Announcements", new[] { "UserId" });
            DropTable("dbo.AnnouncementUsers");
            DropTable("dbo.Announcements");
        }
    }
}

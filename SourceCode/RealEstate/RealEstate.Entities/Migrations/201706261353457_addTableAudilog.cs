namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableAudilog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        AuditLogID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        LogType = c.Int(nullable: false),
                        Description = c.String(),
                        IPAddress = c.String(maxLength: 256),
                        Device = c.String(maxLength: 256),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.AuditLogID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}

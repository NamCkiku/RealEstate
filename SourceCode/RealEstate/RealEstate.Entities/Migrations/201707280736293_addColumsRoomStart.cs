namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumsRoomStart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MoreInfomation", "Facade", c => c.Int());
            AddColumn("dbo.Room", "RoomStar", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Room", "RoomStar");
            DropColumn("dbo.MoreInfomation", "Facade");
        }
    }
}

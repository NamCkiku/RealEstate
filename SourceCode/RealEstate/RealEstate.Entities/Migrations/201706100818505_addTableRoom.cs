namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableRoom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MoreInfomation",
                c => new
                    {
                        MoreInfomationID = c.Int(nullable: false, identity: true),
                        FloorNumber = c.String(),
                        ToiletNumber = c.String(),
                        BedroomNumber = c.String(),
                        Compass = c.String(),
                        ElectricPrice = c.Decimal(precision: 18, scale: 2),
                        WaterPrice = c.Decimal(precision: 18, scale: 2),
                        Convenient = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.MoreInfomationID);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomName = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        RoomTypeID = c.Int(nullable: false),
                        WardID = c.Int(),
                        DistrictID = c.Int(),
                        ProvinceID = c.Int(nullable: false),
                        VipID = c.Int(),
                        MoreInfomationID = c.Int(),
                        PaymentID = c.Int(),
                        ThumbnailImage = c.String(nullable: false, maxLength: 256),
                        MoreImages = c.String(storeType: "xml"),
                        Acreage = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 256),
                        Sex = c.String(maxLength: 10),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 256),
                        UserID = c.String(nullable: false),
                        Description = c.String(maxLength: 500),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Lat = c.Double(),
                        Lng = c.Double(),
                        DisplayOrder = c.Int(),
                        ViewCount = c.Int(),
                        MetaKeyword = c.String(maxLength: 400),
                        MetaDescription = c.String(),
                        MetaTitle = c.String(maxLength: 400),
                        Tags = c.String(),
                        Published = c.Boolean(nullable: false),
                        isDelete = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoomType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomTypeName = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        ImageIcon = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        isDelete = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoomType");
            DropTable("dbo.Room");
            DropTable("dbo.MoreInfomation");
        }
    }
}

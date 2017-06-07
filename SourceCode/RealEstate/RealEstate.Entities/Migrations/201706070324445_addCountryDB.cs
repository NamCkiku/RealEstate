namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCountryDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(nullable: false, maxLength: 100),
                        CommonName = c.String(maxLength: 100),
                        FormalName = c.String(maxLength: 100),
                        CountryType = c.String(maxLength: 100),
                        CountrySubType = c.String(maxLength: 100),
                        Sovereignty = c.String(maxLength: 100),
                        Capital = c.String(maxLength: 100),
                        CurrencyCode = c.String(maxLength: 100),
                        CurrencyName = c.String(maxLength: 100),
                        TelephoneCode = c.String(maxLength: 100),
                        CountryCode3 = c.String(maxLength: 100),
                        CountryNumber = c.String(maxLength: 100),
                        InternetCountryCode = c.String(maxLength: 100),
                        SortOrder = c.Int(),
                        IsPublished = c.Boolean(),
                        Flags = c.String(maxLength: 50),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Type = c.String(maxLength: 50),
                        LatiLongTude = c.String(maxLength: 50),
                        ProvinceId = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Province", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Type = c.String(maxLength: 20),
                        TelephoneCode = c.Int(),
                        ZipCode = c.String(maxLength: 20),
                        CountryId = c.Int(nullable: false),
                        CountryCode = c.String(maxLength: 2),
                        SortOrder = c.Int(),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Ward",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.String(maxLength: 50),
                        LatiLongTude = c.String(maxLength: 50),
                        DistrictID = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.District", t => t.DistrictID, cascadeDelete: true)
                .Index(t => t.DistrictID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ward", "DistrictID", "dbo.District");
            DropForeignKey("dbo.District", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Province", "CountryId", "dbo.Country");
            DropIndex("dbo.Ward", new[] { "DistrictID" });
            DropIndex("dbo.Province", new[] { "CountryId" });
            DropIndex("dbo.District", new[] { "ProvinceId" });
            DropTable("dbo.Ward");
            DropTable("dbo.Province");
            DropTable("dbo.District");
            DropTable("dbo.Country");
        }
    }
}

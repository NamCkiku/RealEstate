namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addwallets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTransactionHistory",
                c => new
                    {
                        WalletTransactionID = c.Int(nullable: false, identity: true),
                        WalletID = c.Int(nullable: false),
                        WalletTransactionTypeID = c.Int(nullable: false),
                        UserID = c.String(),
                        DateTransaction = c.DateTime(nullable: false),
                        AmountTransaction = c.Single(nullable: false),
                        AmountCurrent = c.Single(nullable: false),
                        BeginBalance = c.Single(nullable: false),
                        Note = c.String(),
                        IsVerified = c.Boolean(),
                        VerifiedByUser = c.String(),
                        VerifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(),
                        ReasonOfDeleted = c.String(),
                        DeletedDate = c.DateTime(),
                        CreatedByUser = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedByUser = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.WalletTransactionID);
            
            CreateTable(
                "dbo.UserWallet",
                c => new
                    {
                        WalletID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        WalletName = c.String(nullable: false, maxLength: 256),
                        Amount = c.Double(),
                        PromotionAmount = c.Double(),
                        PromotionExprireDate = c.DateTime(),
                        IsLocked = c.Boolean(),
                        ReasonLocked = c.String(),
                        IsDeleted = c.Boolean(),
                        ReasonDeleted = c.String(),
                        CreatedByUser = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedByUser = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.WalletID);
            
            CreateTable(
                "dbo.WalletTransactionTypes",
                c => new
                    {
                        WalletTransactionTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TypeTransaction = c.Boolean(),
                        Description = c.String(),
                        IsWeb = c.Boolean(),
                        IsAuto = c.Boolean(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedByUser = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedByUser = c.Guid(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.WalletTransactionTypeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WalletTransactionTypes");
            DropTable("dbo.UserWallet");
            DropTable("dbo.UserTransactionHistory");
        }
    }
}

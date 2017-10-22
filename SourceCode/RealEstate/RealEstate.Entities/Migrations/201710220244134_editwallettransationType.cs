namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editwallettransationType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WalletTransactionTypes", "TypeTransaction", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WalletTransactionTypes", "TypeTransaction", c => c.Boolean());
        }
    }
}

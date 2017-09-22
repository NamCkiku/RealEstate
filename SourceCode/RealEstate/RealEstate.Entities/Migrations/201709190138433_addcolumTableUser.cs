namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumTableUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Coin", c => c.Int(nullable: false));
            AddColumn("dbo.AppUsers", "RewardPoint", c => c.Int(nullable: false));
            AddColumn("dbo.AppUsers", "RankStar", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "RankStar");
            DropColumn("dbo.AppUsers", "RewardPoint");
            DropColumn("dbo.AppUsers", "Coin");
        }
    }
}

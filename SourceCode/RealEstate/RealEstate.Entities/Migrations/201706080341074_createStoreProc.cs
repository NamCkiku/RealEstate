namespace RealEstate.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class createStoreProc : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "spGetAllCountry",
                @"SELECT * FROM [Country]"
              );
        }

        public override void Down()
        {
            DropStoredProcedure("spGetAllCountry");
        }
    }
}

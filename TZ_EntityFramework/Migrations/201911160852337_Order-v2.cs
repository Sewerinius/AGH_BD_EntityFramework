namespace TZ_EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orderv2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "UnitPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

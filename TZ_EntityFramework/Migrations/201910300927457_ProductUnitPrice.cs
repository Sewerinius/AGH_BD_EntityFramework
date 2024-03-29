namespace TZ_EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductUnitPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UnitPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UnitPrice");
        }
    }
}

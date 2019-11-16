namespace TZ_EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Productv2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "category_CategoryID" });
            RenameColumn(table: "dbo.Products", name: "category_CategoryID", newName: "CategoryID");
            AlterColumn("dbo.Products", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryID");
            AddForeignKey("dbo.Products", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            AlterColumn("dbo.Products", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "CategoryID", newName: "category_CategoryID");
            CreateIndex("dbo.Products", "category_CategoryID");
            AddForeignKey("dbo.Products", "category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}

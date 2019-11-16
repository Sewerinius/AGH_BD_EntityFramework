namespace TZ_EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedintcategoryIDtoCategorycategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "category_CategoryID", c => c.Int());
            CreateIndex("dbo.Products", "category_CategoryID");
            AddForeignKey("dbo.Products", "category_CategoryID", "dbo.Categories", "CategoryID");
            DropColumn("dbo.Products", "CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "category_CategoryID" });
            DropColumn("dbo.Products", "category_CategoryID");
        }
    }
}

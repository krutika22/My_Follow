namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductOwnersID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProductOwnersID");
            AddForeignKey("dbo.Products", "ProductOwnersID", "dbo.ProductOwners", "ProductOwnersID", cascadeDelete: true);
            DropColumn("dbo.Updates", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Updates", "Photo", c => c.String());
            DropForeignKey("dbo.Products", "ProductOwnersID", "dbo.ProductOwners");
            DropIndex("dbo.Products", new[] { "ProductOwnersID" });
            DropColumn("dbo.Products", "ProductOwnersID");
        }
    }
}

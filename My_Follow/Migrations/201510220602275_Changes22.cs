namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Updates", "EndUsersID", "dbo.EndUsers");
            DropForeignKey("dbo.Updates", "ProductOwnersID", "dbo.ProductOwners");
            DropIndex("dbo.Updates", new[] { "ProductOwnersID" });
            DropIndex("dbo.Updates", new[] { "EndUsersID" });
            AddColumn("dbo.Products", "Details", c => c.String());
            DropColumn("dbo.Updates", "ProductOwnersID");
            DropColumn("dbo.Updates", "EndUsersID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Updates", "EndUsersID", c => c.Int(nullable: false));
            AddColumn("dbo.Updates", "ProductOwnersID", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Details");
            CreateIndex("dbo.Updates", "EndUsersID");
            CreateIndex("dbo.Updates", "ProductOwnersID");
            AddForeignKey("dbo.Updates", "ProductOwnersID", "dbo.ProductOwners", "ProductOwnersID", cascadeDelete: true);
            AddForeignKey("dbo.Updates", "EndUsersID", "dbo.EndUsers", "EndUsersID", cascadeDelete: true);
        }
    }
}

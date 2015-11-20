namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EndUsers", "ProductID", "dbo.Products");
            DropForeignKey("dbo.EndUsers", "UpdatesID", "dbo.Updates");
            DropIndex("dbo.EndUsers", new[] { "UpdatesID" });
            DropIndex("dbo.EndUsers", new[] { "ProductID" });
            AddColumn("dbo.Products", "EndUsersID", c => c.Int(nullable: false));
            AddColumn("dbo.Updates", "EndUsersID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "EndUsersID");
            CreateIndex("dbo.Updates", "EndUsersID");
            AddForeignKey("dbo.Products", "EndUsersID", "dbo.EndUsers", "EndUsersID", cascadeDelete: true);
            AddForeignKey("dbo.Updates", "EndUsersID", "dbo.EndUsers", "EndUsersID", cascadeDelete: true);
            DropColumn("dbo.EndUsers", "UpdatesID");
            DropColumn("dbo.EndUsers", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EndUsers", "ProductID", c => c.Int(nullable: false));
            AddColumn("dbo.EndUsers", "UpdatesID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Updates", "EndUsersID", "dbo.EndUsers");
            DropForeignKey("dbo.Products", "EndUsersID", "dbo.EndUsers");
            DropIndex("dbo.Updates", new[] { "EndUsersID" });
            DropIndex("dbo.Products", new[] { "EndUsersID" });
            DropColumn("dbo.Updates", "EndUsersID");
            DropColumn("dbo.Products", "EndUsersID");
            CreateIndex("dbo.EndUsers", "ProductID");
            CreateIndex("dbo.EndUsers", "UpdatesID");
            AddForeignKey("dbo.EndUsers", "UpdatesID", "dbo.Updates", "UpdatesID", cascadeDelete: true);
            AddForeignKey("dbo.EndUsers", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
        }
    }
}

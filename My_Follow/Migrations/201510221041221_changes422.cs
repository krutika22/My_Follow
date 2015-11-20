namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes422 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "EndUsersID", "dbo.EndUsers");
            DropIndex("dbo.Products", new[] { "EndUsersID" });
            DropColumn("dbo.Products", "EndUsersID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "EndUsersID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "EndUsersID");
            AddForeignKey("dbo.Products", "EndUsersID", "dbo.EndUsers", "EndUsersID", cascadeDelete: true);
        }
    }
}

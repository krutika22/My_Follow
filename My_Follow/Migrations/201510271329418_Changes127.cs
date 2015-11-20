namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes127 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductEndUsers",
                c => new
                    {
                        Product_ProductID = c.Int(nullable: false),
                        EndUsers_EndUsersID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductID, t.EndUsers_EndUsersID })
                .ForeignKey("dbo.Products", t => t.Product_ProductID, cascadeDelete: true)
                .ForeignKey("dbo.EndUsers", t => t.EndUsers_EndUsersID, cascadeDelete: true)
                .Index(t => t.Product_ProductID)
                .Index(t => t.EndUsers_EndUsersID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductEndUsers", "EndUsers_EndUsersID", "dbo.EndUsers");
            DropForeignKey("dbo.ProductEndUsers", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.ProductEndUsers", new[] { "EndUsers_EndUsersID" });
            DropIndex("dbo.ProductEndUsers", new[] { "Product_ProductID" });
            DropTable("dbo.ProductEndUsers");
        }
    }
}

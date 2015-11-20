namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes203 : DbMigration
    {
        public override void Up()
        {

            CreateTable(
     "dbo.Followers",
     c => new
     {
         ProductID = c.Int(nullable: false),
         EndUsersID = c.Int(nullable: false),
     })
     .PrimaryKey(t => new { t.ProductID, t.EndUsersID })
     .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
     .ForeignKey("dbo.EndUsers", t => t.EndUsersID, cascadeDelete: true)
     .Index(t => t.ProductID)
     .Index(t => t.EndUsersID);
        }
        
        public override void Down()
        {
        }
    }
}

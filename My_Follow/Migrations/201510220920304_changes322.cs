namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes322 : DbMigration
    {
        public override void Up()
        {
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Media", "UpdatesID", "dbo.Updates");
            DropIndex("dbo.Media", new[] { "UpdatesID" });
            DropTable("dbo.Media");
        }
    }
}

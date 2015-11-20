namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes131 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "Video", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "Video");
        }
    }
}

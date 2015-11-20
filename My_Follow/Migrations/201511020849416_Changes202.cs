namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes202 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Media", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Media", "Image", c => c.String());
        }
    }
}

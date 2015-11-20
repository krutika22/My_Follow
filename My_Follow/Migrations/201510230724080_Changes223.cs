namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes223 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "Image");
        }
    }
}

namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes23 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Media", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Media", "Photo", c => c.String());
        }
    }
}

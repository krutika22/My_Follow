namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes130 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EndUsers", "Name", c => c.String());
            AddColumn("dbo.ProductOwners", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductOwners", "Name");
            DropColumn("dbo.EndUsers", "Name");
        }
    }
}

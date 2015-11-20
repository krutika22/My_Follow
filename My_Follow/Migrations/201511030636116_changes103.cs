namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes103 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductEndUsers", newName: "Followers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Followers", newName: "ProductEndUsers");
        }
    }
}

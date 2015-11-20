namespace My_Follow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes102 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products", newName: "Product");
            RenameTable(name: "dbo.InvitationForms", newName: "InvitationForm");
            RenameColumn(table: "dbo.ProductEndUsers", name: "Product_ProductID", newName: "ProductID");
            RenameColumn(table: "dbo.ProductEndUsers", name: "EndUsers_EndUsersID", newName: "EndUsersID");
            RenameIndex(table: "dbo.ProductEndUsers", name: "IX_Product_ProductID", newName: "IX_ProductID");
            RenameIndex(table: "dbo.ProductEndUsers", name: "IX_EndUsers_EndUsersID", newName: "IX_EndUsersID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductEndUsers", name: "IX_EndUsersID", newName: "IX_EndUsers_EndUsersID");
            RenameIndex(table: "dbo.ProductEndUsers", name: "IX_ProductID", newName: "IX_Product_ProductID");
            RenameColumn(table: "dbo.ProductEndUsers", name: "EndUsersID", newName: "EndUsers_EndUsersID");
            RenameColumn(table: "dbo.ProductEndUsers", name: "ProductID", newName: "Product_ProductID");
            RenameTable(name: "dbo.InvitationForm", newName: "InvitationForms");
            RenameTable(name: "dbo.Product", newName: "Products");
        }
    }
}

namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAspNetTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetRoles", newName: "Role");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRole");
            RenameTable(name: "dbo.AspNetUsers", newName: "AllUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AllUsers", newName: "AspNetUsers");
            RenameTable(name: "dbo.UserRole", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Role", newName: "AspNetRoles");
        }
    }
}

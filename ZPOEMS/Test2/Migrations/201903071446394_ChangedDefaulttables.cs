namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDefaulttables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetRoles", newName: "Role");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRole");
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "DepartmentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DepartmentId");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "FirstName");
            RenameTable(name: "dbo.UserRole", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Role", newName: "AspNetRoles");
        }
    }
}

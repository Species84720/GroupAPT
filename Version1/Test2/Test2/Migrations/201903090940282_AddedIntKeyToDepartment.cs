namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIntKeyToDepartment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "DepartmentParent", "dbo.Departments");
            DropIndex("dbo.Departments", new[] { "DepartmentParent" });
            DropIndex("dbo.AllUsers", new[] {"DepartmentId"});

            DropColumn("dbo.Departments", "DepartmentParent");
            AddColumn("dbo.Departments", "DepartmentParentId", c => c.Int(nullable: true));

            DropPrimaryKey("dbo.Departments");
            AddColumn("dbo.Departments", "DepartmentName", c => c.String(nullable: false));

            DropColumn("dbo.Departments", "DepartmentId");
            AddColumn("dbo.Departments", "DepartmentId", c => c.Int(nullable: true, identity: true));

            AlterColumn("dbo.AllUsers", "DepartmentId", c => c.Int(nullable: true));

            AddPrimaryKey("dbo.Departments", "DepartmentId");
            CreateIndex("dbo.AllUsers", "DepartmentId");
            CreateIndex("dbo.Departments", "DepartmentParentId");
            

            AddForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
            AddForeignKey("dbo.Departments", "DepartmentParentId", "dbo.Departments", "DepartmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "DepartmentParentId", "dbo.Departments");
            DropForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Departments", new[] { "DepartmentParentId" });
            DropIndex("dbo.AllUsers", new[] { "DepartmentId" });
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.AllUsers", "DepartmentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Departments", "DepartmentParentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Departments", "DepartmentId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Departments", "DepartmentName");
            AddPrimaryKey("dbo.Departments", "DepartmentId");
            RenameColumn(table: "dbo.Departments", name: "DepartmentParentId", newName: "DepartmentParent");
            CreateIndex("dbo.AllUsers", "DepartmentId");
            CreateIndex("dbo.Departments", "DepartmentParent");
            AddForeignKey("dbo.Departments", "DepartmentParent", "dbo.Departments", "DepartmentId");
            AddForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments", "DepartmentId");
        }
    }
}

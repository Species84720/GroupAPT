namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeemsNecessary : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.AllUsers", new[] { "DepartmentId" });
            DropIndex("dbo.Departments", new[] { "DepartmentParentId" });
            AlterColumn("dbo.AllUsers", "DepartmentId", c => c.Int());
            AlterColumn("dbo.Departments", "DepartmentParentId", c => c.Int());
            CreateIndex("dbo.AllUsers", "DepartmentId");
            CreateIndex("dbo.Departments", "DepartmentParentId");
            AddForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments", "DepartmentId");

            Sql(@" SET IDENTITY_INSERT[dbo].[Departments]
            ON
                INSERT INTO[dbo].[Departments]
                ([DepartmentParentId], [DepartmentName], [DepartmentId]) VALUES(NULL, N'ICT Faculty', 1)
            INSERT INTO[dbo].[Departments]
                ([DepartmentParentId], [DepartmentName], [DepartmentId]) VALUES(1, N'Computer Science', 2)
            INSERT INTO[dbo].[Departments]
                ([DepartmentParentId], [DepartmentName], [DepartmentId]) VALUES(1, N'Computer Engineering', 3)
            INSERT INTO[dbo].[Departments]
                ([DepartmentParentId], [DepartmentName], [DepartmentId]) VALUES(1, N'Software Development', 4)
            INSERT INTO[dbo].[Departments]
                ([DepartmentParentId], [DepartmentName], [DepartmentId]) VALUES(1, N'Artificial Intelligence', 5)
            SET IDENTITY_INSERT[dbo].[Departments] OFF
            ");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Departments", new[] { "DepartmentParentId" });
            DropIndex("dbo.AllUsers", new[] { "DepartmentId" });
            AlterColumn("dbo.Departments", "DepartmentParentId", c => c.Int(nullable: false));
            AlterColumn("dbo.AllUsers", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Departments", "DepartmentParentId");
            CreateIndex("dbo.AllUsers", "DepartmentId");
            AddForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
        }
    }
}

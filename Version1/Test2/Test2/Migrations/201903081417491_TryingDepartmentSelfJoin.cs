namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingDepartmentSelfJoin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.String(nullable: false, maxLength: 128),
                        DepartmentName = c.String(),
                        DepartmentParent = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Departments", t => t.DepartmentParent)
                .Index(t => t.DepartmentParent);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "DepartmentParent", "dbo.Departments");
            DropIndex("dbo.Departments", new[] { "DepartmentParent" });
            DropTable("dbo.Departments");
        }
    }
}

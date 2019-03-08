namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentIdAsForeignKeyInIdentityModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AllUsers", "DepartmentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AllUsers", "DepartmentId");
            AddForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments", "DepartmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AllUsers", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.AllUsers", new[] { "DepartmentId" });
            AlterColumn("dbo.AllUsers", "DepartmentId", c => c.String());
        }
    }
}

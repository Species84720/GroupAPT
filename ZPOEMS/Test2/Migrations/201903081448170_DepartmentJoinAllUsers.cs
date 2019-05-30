namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentJoinAllUsers : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Departments", "DepartmentName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "DepartmentName", c => c.String());
        }
    }
}

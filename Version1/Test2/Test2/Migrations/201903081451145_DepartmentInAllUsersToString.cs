namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentInAllUsersToString : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AllUsers", "DepartmentId");
            AddColumn("dbo.AllUsers", "DepartmentId", c => c.String());



        }

        public override void Down()
        {
           
        }
    }
}

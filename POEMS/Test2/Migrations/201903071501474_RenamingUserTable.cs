namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamingUserTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "AllUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AllUsers", newName: "AspNetUsers");
        }
    }
}

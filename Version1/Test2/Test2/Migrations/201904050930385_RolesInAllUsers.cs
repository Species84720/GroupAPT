namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesInAllUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllUsers", "Role", c => c.String());
            DropColumn("dbo.AllUsers", "NickName");


            
        }
        
        public override void Down()
        {
            AddColumn("dbo.AllUsers", "NickName", c => c.String());
            DropColumn("dbo.AllUsers", "Role");
        }
    }
}

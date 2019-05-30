namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNicknameToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllUsers", "NickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllUsers", "NickName");
        }
    }
}

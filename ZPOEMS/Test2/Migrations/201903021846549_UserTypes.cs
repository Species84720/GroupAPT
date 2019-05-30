namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.ID);

           

        }
        
        public override void Down()
        {
            DropTable("dbo.UserTypes");
        }
    }
}

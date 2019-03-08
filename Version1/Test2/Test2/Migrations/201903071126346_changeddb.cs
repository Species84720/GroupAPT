namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddb : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserTypes");
        }
        
        public override void Down()
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
    }
}

namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLogs1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        WhoId = c.String(maxLength: 128),
                        When = c.DateTime(nullable: false),
                        Activity = c.String(),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.AllUsers", t => t.WhoId)
                .Index(t => t.WhoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "WhoId", "dbo.AllUsers");
            DropIndex("dbo.Logs", new[] { "WhoId" });
            DropTable("dbo.Logs");
        }
    }
}

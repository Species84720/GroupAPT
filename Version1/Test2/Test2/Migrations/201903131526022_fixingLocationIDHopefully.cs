namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingLocationIDHopefully : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExamSessions", "LocationId", "dbo.Locations");
            DropPrimaryKey("dbo.Locations");
           
            DropColumn("dbo.Locations", "LocationId");
            AddColumn("dbo.Locations", "LocationId", c => c.Int(nullable: true, identity: true));

            AddPrimaryKey("dbo.Locations", "LocationId");
            AddForeignKey("dbo.ExamSessions", "LocationId", "dbo.Locations", "LocationId", cascadeDelete: false);

        }
        
        public override void Down()
        {
        }
    }
}

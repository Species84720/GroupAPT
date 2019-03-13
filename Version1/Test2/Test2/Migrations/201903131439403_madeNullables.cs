namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeNullables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enrollments", "ExamMark", c => c.Byte(nullable: true));
            AlterColumn("dbo.Enrollments", "SeatNumber", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enrollments", "SeatNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Enrollments", "ExamMark", c => c.Byte(nullable: false));
        }
    }
}

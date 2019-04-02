namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teaching1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachings",
                c => new
                    {
                        TeachingId = c.Int(nullable: false, identity: true),
                        ExaminerId = c.String(maxLength: 128),
                        SubjectId = c.String(maxLength: 128),
                        Subject_SubjectId = c.String(maxLength: 128),
                        Subject_SubjectId1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TeachingId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .ForeignKey("dbo.AllUsers", t => t.ExaminerId)
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectId)
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectId1)
                .Index(t => t.ExaminerId)
                .Index(t => t.SubjectId)
                .Index(t => t.Subject_SubjectId)
                .Index(t => t.Subject_SubjectId1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachings", "Subject_SubjectId1", "dbo.Subjects");
            DropForeignKey("dbo.Teachings", "Subject_SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Teachings", "ExaminerId", "dbo.AllUsers");
            DropForeignKey("dbo.Teachings", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.Teachings", new[] { "Subject_SubjectId1" });
            DropIndex("dbo.Teachings", new[] { "Subject_SubjectId" });
            DropIndex("dbo.Teachings", new[] { "SubjectId" });
            DropIndex("dbo.Teachings", new[] { "ExaminerId" });
            DropTable("dbo.Teachings");
        }
    }
}

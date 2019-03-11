namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teaching2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teachings", "Subject_SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Teachings", "Subject_SubjectId1", "dbo.Subjects");
            DropIndex("dbo.Teachings", new[] { "Subject_SubjectId" });
            DropIndex("dbo.Teachings", new[] { "Subject_SubjectId1" });

            DropColumn("dbo.Teachings", "Subject_SubjectId");
            DropColumn("dbo.Teachings", "Subject_SubjectId1");
            //AddForeignKey("dbo.Teachings", "SubjectId", "dbo.Subjects", "SubjectId");

        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachings", "Subject_SubjectId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Teachings", name: "SubjectId", newName: "Subject_SubjectId1");
            AddColumn("dbo.Teachings", "SubjectId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Teachings", "Subject_SubjectId1");
            CreateIndex("dbo.Teachings", "Subject_SubjectId");
            AddForeignKey("dbo.Teachings", "Subject_SubjectId", "dbo.Subjects", "SubjectId");
        }
    }
}

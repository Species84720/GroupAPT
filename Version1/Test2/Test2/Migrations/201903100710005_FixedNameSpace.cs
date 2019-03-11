namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedNameSpace : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExamSessions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Questions", "SubjectCode", "dbo.Subjects");
            DropForeignKey("dbo.Topics", "SubjectCode", "dbo.Subjects");
            DropForeignKey("dbo.Enrollments", "SubjectCode", "dbo.Subjects");
            RenameColumn(table: "dbo.Enrollments", name: "SubjectCode", newName: "SubjectId");
            RenameColumn(table: "dbo.Questions", name: "SubjectCode", newName: "SubjectId");
            RenameColumn(table: "dbo.Topics", name: "SubjectCode", newName: "SubjectId");
            RenameIndex(table: "dbo.Enrollments", name: "IX_SubjectCode", newName: "IX_SubjectId");
            RenameIndex(table: "dbo.Questions", name: "IX_SubjectCode", newName: "IX_SubjectId");
            RenameIndex(table: "dbo.Topics", name: "IX_SubjectCode", newName: "IX_SubjectId");
            DropPrimaryKey("dbo.Subjects");
            AddColumn("dbo.Subjects", "SubjectId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Subjects", "SubjectId");
            AddForeignKey("dbo.ExamSessions", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: false);
            AddForeignKey("dbo.Topics", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: false);
            AddForeignKey("dbo.Enrollments", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: false);
            DropColumn("dbo.Subjects", "SubjectCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "SubjectCode", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Enrollments", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Topics", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ExamSessions", "SubjectId", "dbo.Subjects");
            DropPrimaryKey("dbo.Subjects");
            DropColumn("dbo.Subjects", "SubjectId");
            AddPrimaryKey("dbo.Subjects", "SubjectCode");
            RenameIndex(table: "dbo.Topics", name: "IX_SubjectId", newName: "IX_SubjectCode");
            RenameIndex(table: "dbo.Questions", name: "IX_SubjectId", newName: "IX_SubjectCode");
            RenameIndex(table: "dbo.Enrollments", name: "IX_SubjectId", newName: "IX_SubjectCode");
            RenameColumn(table: "dbo.Topics", name: "SubjectId", newName: "SubjectCode");
            RenameColumn(table: "dbo.Questions", name: "SubjectId", newName: "SubjectCode");
            RenameColumn(table: "dbo.Enrollments", name: "SubjectId", newName: "SubjectCode");
            AddForeignKey("dbo.Enrollments", "SubjectCode", "dbo.Subjects", "SubjectCode", cascadeDelete: true);
            AddForeignKey("dbo.Topics", "SubjectCode", "dbo.Subjects", "SubjectCode", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "SubjectCode", "dbo.Subjects", "SubjectCode", cascadeDelete: true);
            AddForeignKey("dbo.ExamSessions", "SubjectId", "dbo.Subjects", "SubjectCode", cascadeDelete: true);
        }
    }
}

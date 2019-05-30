namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingSomeIssues : DbMigration
    {
        public override void Up()
        {

            DropIndex("dbo.Enrollments", new[] { "Student_StudentId" });
            DropIndex("dbo.Enrollments", new[] { "Student_StudentId1" });
            DropIndex("dbo.Enrollments", new[] { "Subject_SubjectCode" });
           
            
            DropIndex("dbo.Invigilations", new[] { "Exam_ExamId" });
            DropIndex("dbo.Invigilations", new[] { "ExamSession_ExamId" });
             DropForeignKey("dbo.Invigilations", "Exam_ExamId", "dbo.ExamSessions");
            DropForeignKey("dbo.Invigilations", "UserId", "dbo.AllUsers");
            DropForeignKey("dbo.Enrollments", "Student_StudentId1", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "Subject_SubjectCode", "dbo.Subjects");
            DropForeignKey("dbo.Enrollments", "Subject_SubjectCode1", "dbo.Subjects");
            DropForeignKey("dbo.Invigilations", "ExamSession_ExamId", "dbo.ExamSessions");
            DropForeignKey("dbo.Enrollments", "Student_StudentId", "dbo.Students");

            
            DropColumn("dbo.Enrollments", "Student_StudentId");
            DropColumn("dbo.Invigilations", "ExamSession_ExamId");
            
            


            DropPrimaryKey("dbo.StudentAnswers");
            DropPrimaryKey("dbo.Shots");
            
            AlterColumn("dbo.StudentAnswers", "AnswerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Invigilations", "ExamId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Shots", "ShotId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StudentAnswers", "AnswerId");
            AddPrimaryKey("dbo.Shots", "ShotId");
            //CreateIndex("dbo.Enrollments", "UserId");
            //CreateIndex("dbo.Enrollments", "SubjectCode");
           // CreateIndex("dbo.Invigilations", "ExamId");
            //AddForeignKey("dbo.Enrollments", "SubjectCode", "dbo.Subjects", "SubjectCode", cascadeDelete: true);
            //AddForeignKey("dbo.Invigilations", "ExamId", "dbo.ExamSessions", "ExamId", cascadeDelete: true);
           // AddForeignKey("dbo.Enrollments", "UserId", "dbo.Students", "StudentId", cascadeDelete: true);
            DropColumn("dbo.Enrollments", "Student_StudentId1");
            DropColumn("dbo.Enrollments", "Subject_SubjectCode");
            DropColumn("dbo.Invigilations", "Exam_ExamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invigilations", "Exam_ExamId", c => c.String(maxLength: 128));
            AddColumn("dbo.Enrollments", "Subject_SubjectCode", c => c.String(maxLength: 128));
            AddColumn("dbo.Enrollments", "Student_StudentId1", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Enrollments", "UserId", "dbo.Students");
            DropForeignKey("dbo.Invigilations", "ExamId", "dbo.ExamSessions");
            DropForeignKey("dbo.Enrollments", "SubjectCode", "dbo.Subjects");
            DropIndex("dbo.Invigilations", new[] { "ExamId" });
            DropIndex("dbo.Enrollments", new[] { "SubjectCode" });
            DropIndex("dbo.Enrollments", new[] { "UserId" });
            DropPrimaryKey("dbo.Shots");
            DropPrimaryKey("dbo.StudentAnswers");
            AlterColumn("dbo.Shots", "ShotId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Invigilations", "ExamId", c => c.String(maxLength: 128));
            AlterColumn("dbo.StudentAnswers", "AnswerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Enrollments", "SubjectCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.Enrollments", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Shots", "ShotId");
            AddPrimaryKey("dbo.StudentAnswers", "AnswerId");
            RenameColumn(table: "dbo.Enrollments", name: "UserId", newName: "Student_StudentId");
            RenameColumn(table: "dbo.Invigilations", name: "ExamId", newName: "ExamSession_ExamId");
            RenameColumn(table: "dbo.Enrollments", name: "SubjectCode", newName: "Subject_SubjectCode1");
            AddColumn("dbo.Invigilations", "ExamId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Enrollments", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Enrollments", "SubjectCode", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Invigilations", "ExamSession_ExamId");
            CreateIndex("dbo.Invigilations", "Exam_ExamId");
            CreateIndex("dbo.Invigilations", "ExamId");
            CreateIndex("dbo.Enrollments", "Subject_SubjectCode1");
            CreateIndex("dbo.Enrollments", "Subject_SubjectCode");
            CreateIndex("dbo.Enrollments", "Student_StudentId1");
            CreateIndex("dbo.Enrollments", "Student_StudentId");
            CreateIndex("dbo.Enrollments", "SubjectCode");
            CreateIndex("dbo.Enrollments", "UserId");
            AddForeignKey("dbo.Enrollments", "Student_StudentId", "dbo.Students", "StudentId");
            AddForeignKey("dbo.Invigilations", "ExamSession_ExamId", "dbo.ExamSessions", "ExamId");
            AddForeignKey("dbo.Enrollments", "Subject_SubjectCode1", "dbo.Subjects", "SubjectCode");
            AddForeignKey("dbo.Enrollments", "Subject_SubjectCode", "dbo.Subjects", "SubjectCode");
            AddForeignKey("dbo.Enrollments", "Student_StudentId1", "dbo.Students", "StudentId");
            AddForeignKey("dbo.Invigilations", "UserId", "dbo.AllUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Invigilations", "Exam_ExamId", "dbo.ExamSessions", "ExamId");
        }
    }
}

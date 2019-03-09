namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingManyDBTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectCode = c.String(nullable: false, maxLength: 128),
                        SubjectName = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Credits = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectCode)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        EnrollmentId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        SubjectCode = c.String(nullable: false, maxLength: 128),
                        ExamMark = c.Byte(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        SessionStatus = c.Int(nullable: false),
                        FinalAssessment = c.Int(nullable: false),
                        Student_StudentId = c.String(maxLength: 128),
                        Student_StudentId1 = c.String(maxLength: 128),
                        Subject_SubjectCode = c.String(maxLength: 128),
                        Subject_SubjectCode1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EnrollmentId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .ForeignKey("dbo.Students", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectCode, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_StudentId1)
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectCode)
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectCode1)
                .Index(t => t.UserId)
                .Index(t => t.SubjectCode)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Student_StudentId1)
                .Index(t => t.Subject_SubjectCode)
                .Index(t => t.Subject_SubjectCode1);
            
            CreateTable(
                "dbo.StudentAnswers",
                c => new
                    {
                        AnswerId = c.String(nullable: false, maxLength: 128),
                        EnrollmentId = c.String(nullable: false, maxLength: 128),
                        PaperQuestionId = c.String(nullable: false, maxLength: 128),
                        CorrectorId = c.String(maxLength: 128),
                        Answer = c.String(),
                        ExaminerComments = c.String(),
                        MarksGained = c.Byte(nullable: false),
                        CorrectedDateTime = c.DateTime(nullable: false),
                        CommittedByStudent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.AllUsers", t => t.CorrectorId)
                .ForeignKey("dbo.Enrollments", t => t.EnrollmentId, cascadeDelete: true)
                .ForeignKey("dbo.PaperQuestions", t => t.PaperQuestionId, cascadeDelete: true)
                .Index(t => t.EnrollmentId)
                .Index(t => t.PaperQuestionId)
                .Index(t => t.CorrectorId);
            
            CreateTable(
                "dbo.PaperQuestions",
                c => new
                    {
                        PaperQuestionId = c.String(nullable: false, maxLength: 128),
                        ExamId = c.String(nullable: false, maxLength: 128),
                        QuestionId = c.String(nullable: false, maxLength: 128),
                        NumberInPaper = c.Byte(nullable: false),
                        MarksAllocated = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.PaperQuestionId)
                .ForeignKey("dbo.ExamSessions", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.ExamId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.ExamSessions",
                c => new
                    {
                        ExamId = c.String(nullable: false, maxLength: 128),
                        SubjectId = c.String(nullable: false, maxLength: 128),
                        LocationId = c.String(maxLength: 128),
                        ExamDateTime = c.DateTime(nullable: false),
                        ExamEndTime = c.DateTime(nullable: false),
                        QuestionAmount = c.Int(nullable: false),
                        AccessCode = c.String(maxLength: 6),
                        CodeIssueDateTime = c.DateTime(nullable: false),
                        FullyCorrected = c.Boolean(nullable: false),
                        MaxMark = c.Byte(nullable: false),
                        MinMark = c.Byte(nullable: false),
                        AvgMark = c.Single(nullable: false),
                        NumOfParticipants = c.Int(nullable: false),
                        NumOfFails = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExamId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.SubjectId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Invigilations",
                c => new
                    {
                        InvigilationId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ExamId = c.String(nullable: false, maxLength: 128),
                        Exam_ExamId = c.String(maxLength: 128),
                        ExamSession_ExamId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InvigilationId)
                .ForeignKey("dbo.ExamSessions", t => t.Exam_ExamId)
                .ForeignKey("dbo.ExamSessions", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.AllUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.ExamSessions", t => t.ExamSession_ExamId)
                .Index(t => t.UserId)
                .Index(t => t.ExamId)
                .Index(t => t.Exam_ExamId)
                .Index(t => t.ExamSession_ExamId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.String(nullable: false, maxLength: 128),
                        Campus = c.String(),
                        Building = c.String(),
                        Floor = c.String(),
                        Block = c.String(),
                        Room = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.String(nullable: false, maxLength: 128),
                        SubjectCode = c.String(nullable: false, maxLength: 128),
                        TopicId = c.String(nullable: false, maxLength: 128),
                        QuestionUsage = c.Int(nullable: false),
                        QuestionText = c.String(nullable: false),
                        SampleAnswer = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Subjects", t => t.SubjectCode, cascadeDelete: false)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: false)
                .Index(t => t.SubjectCode)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.String(nullable: false, maxLength: 128),
                        TopicName = c.String(nullable: false),
                        SubjectCode = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TopicId)
                .ForeignKey("dbo.Subjects", t => t.SubjectCode, cascadeDelete: true)
                .Index(t => t.SubjectCode);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        FacialImageDirectory = c.String(),
                        FacialImageTitle = c.String(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.AllUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Shots",
                c => new
                    {
                        ShotId = c.String(nullable: false, maxLength: 128),
                        EnrollmentId = c.String(nullable: false, maxLength: 128),
                        ShotTiming = c.DateTime(nullable: false),
                        ImageLocation = c.String(),
                        ImageTitle = c.String(),
                    })
                .PrimaryKey(t => t.ShotId)
                .ForeignKey("dbo.Enrollments", t => t.EnrollmentId, cascadeDelete: true)
                .Index(t => t.EnrollmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Enrollments", "Subject_SubjectCode1", "dbo.Subjects");
            DropForeignKey("dbo.Enrollments", "Subject_SubjectCode", "dbo.Subjects");
            DropForeignKey("dbo.Enrollments", "Student_StudentId1", "dbo.Students");
            DropForeignKey("dbo.Shots", "EnrollmentId", "dbo.Enrollments");
            DropForeignKey("dbo.Enrollments", "SubjectCode", "dbo.Subjects");
            DropForeignKey("dbo.Enrollments", "UserId", "dbo.Students");
            DropForeignKey("dbo.Students", "UserId", "dbo.AllUsers");
            DropForeignKey("dbo.Enrollments", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentAnswers", "PaperQuestionId", "dbo.PaperQuestions");
            DropForeignKey("dbo.PaperQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "SubjectCode", "dbo.Subjects");
            DropForeignKey("dbo.Questions", "SubjectCode", "dbo.Subjects");
            DropForeignKey("dbo.ExamSessions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ExamSessions", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.PaperQuestions", "ExamId", "dbo.ExamSessions");
            DropForeignKey("dbo.Invigilations", "ExamSession_ExamId", "dbo.ExamSessions");
            DropForeignKey("dbo.Invigilations", "UserId", "dbo.AllUsers");
            DropForeignKey("dbo.Invigilations", "ExamId", "dbo.ExamSessions");
            DropForeignKey("dbo.Invigilations", "Exam_ExamId", "dbo.ExamSessions");
            DropForeignKey("dbo.StudentAnswers", "EnrollmentId", "dbo.Enrollments");
            DropForeignKey("dbo.StudentAnswers", "CorrectorId", "dbo.AllUsers");
            DropIndex("dbo.Shots", new[] { "EnrollmentId" });
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.Topics", new[] { "SubjectCode" });
            DropIndex("dbo.Questions", new[] { "TopicId" });
            DropIndex("dbo.Questions", new[] { "SubjectCode" });
            DropIndex("dbo.Invigilations", new[] { "ExamSession_ExamId" });
            DropIndex("dbo.Invigilations", new[] { "Exam_ExamId" });
            DropIndex("dbo.Invigilations", new[] { "ExamId" });
            DropIndex("dbo.Invigilations", new[] { "UserId" });
            DropIndex("dbo.ExamSessions", new[] { "LocationId" });
            DropIndex("dbo.ExamSessions", new[] { "SubjectId" });
            DropIndex("dbo.PaperQuestions", new[] { "QuestionId" });
            DropIndex("dbo.PaperQuestions", new[] { "ExamId" });
            DropIndex("dbo.StudentAnswers", new[] { "CorrectorId" });
            DropIndex("dbo.StudentAnswers", new[] { "PaperQuestionId" });
            DropIndex("dbo.StudentAnswers", new[] { "EnrollmentId" });
            DropIndex("dbo.Enrollments", new[] { "Subject_SubjectCode1" });
            DropIndex("dbo.Enrollments", new[] { "Subject_SubjectCode" });
            DropIndex("dbo.Enrollments", new[] { "Student_StudentId1" });
            DropIndex("dbo.Enrollments", new[] { "Student_StudentId" });
            DropIndex("dbo.Enrollments", new[] { "SubjectCode" });
            DropIndex("dbo.Enrollments", new[] { "UserId" });
            DropIndex("dbo.Subjects", new[] { "DepartmentId" });
            DropTable("dbo.Shots");
            DropTable("dbo.Students");
            DropTable("dbo.Topics");
            DropTable("dbo.Questions");
            DropTable("dbo.Locations");
            DropTable("dbo.Invigilations");
            DropTable("dbo.ExamSessions");
            DropTable("dbo.PaperQuestions");
            DropTable("dbo.StudentAnswers");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Subjects");
        }
    }
}

namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixesForExam : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentAnswers", "CorrectedDateTime", c => c.DateTime(nullable: true));
            AlterColumn("dbo.ExamSessions", "ExamDateTime", c => c.DateTime(nullable: true));
            AlterColumn("dbo.ExamSessions", "ExamEndTime", c => c.DateTime(nullable: true));
            AlterColumn("dbo.ExamSessions", "CodeIssueDateTime", c => c.DateTime(nullable: true));

            Sql(@"INSERT INTO [dbo].[AllUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [Surname], [DepartmentId], [NickName]) VALUES (N'143b7e0b-dc92-46ee-913a-78a4a3db8ec8', N'ss@mail.com', 0, N'AC03J0JMtyheJidHz0UnOV913IGMhQX+GRWXdHCKFNNnCKWXDrobSOv95SxItT7Gpg==', N'c7ed0ade-0a87-4fbc-9831-293a0229eec8', NULL, 0, 0, NULL, 1, 0, N'SteStu19', N'Steve', N'Student', 2, N'SteStu19')
INSERT INTO [dbo].[AllUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [Surname], [DepartmentId], [NickName]) VALUES (N'6dcf7b79-8d07-49f9-8822-289b20b6b0ac', N'inv@mail.com', 0, N'ABurZ1dHLIh1GnrgzVUcQ7YDBqkmm8za/rSvdtT3imybetubTYJLhTMrp3R1fl8R2w==', N'fb6f64dd-21aa-4f25-99ef-2ef452592962', NULL, 0, 0, NULL, 1, 0, N'IvyTes19', N'Ivy', N'TestInvigilator', 1, N'IvyTes19')

INSERT INTO [dbo].[Students] ([StudentId], [UserId], [FacialImageDirectory], [FacialImageTitle]) VALUES (N'SteStu19', N'143b7e0b-dc92-46ee-913a-78a4a3db8ec8', NULL, NULL)

INSERT INTO [dbo].[Subjects] ([SubjectName], [DepartmentId], [Credits], [SubjectId]) VALUES (N'Test Subject', 4, N'5', N'TST2019')

INSERT INTO [dbo].[Enrollments] ([EnrollmentId], [StudentId], [SubjectId], [ExamMark], [SeatNumber], [SessionStatus], [FinalAssessment], [Subject_SubjectCode1]) VALUES (N'SteStu19-TST2019', N'SteStu19', N'TST2019', 0, 0, 0, 0, NULL)


SET IDENTITY_INSERT [dbo].[Topics] ON
INSERT INTO [dbo].[Topics] ([TopicName], [SubjectId], [TopicId]) VALUES (N'Test Topic', N'TST2019', 1)
SET IDENTITY_INSERT [dbo].[Topics] OFF

SET IDENTITY_INSERT [dbo].[Questions] ON
INSERT INTO [dbo].[Questions] ([SubjectId], [TopicId], [QuestionUsage], [QuestionText], [SampleAnswer], [QuestionFormat], [QuestionId]) VALUES (N'TST2019', 1, 0, N'Which is the best Language?', N'Rust', 1, 1)
INSERT INTO [dbo].[Questions] ([SubjectId], [TopicId], [QuestionUsage], [QuestionText], [SampleAnswer], [QuestionFormat], [QuestionId]) VALUES (N'TST2019', 1, 1, N'Which of these is different?', NULL, 1, 2)
INSERT INTO [dbo].[Questions] ([SubjectId], [TopicId], [QuestionUsage], [QuestionText], [SampleAnswer], [QuestionFormat], [QuestionId]) VALUES (N'TST2019', 1, 1, N'Explain God and give 2 examples', N'A supreme being. Example: Zeus, Azathoth', 0, 3)
SET IDENTITY_INSERT [dbo].[Questions] OFF


SET IDENTITY_INSERT [dbo].[MultipleChoices] ON
INSERT INTO [dbo].[MultipleChoices] ([MultipleChoiceId], [OptionText1], [OptionText2], [OptionText3], [OptionText4], [CorrectChoice], [QuestionId]) VALUES (1, N'Rust', N'PHP', N'Ruby', N'Go', 1, 1)
INSERT INTO [dbo].[MultipleChoices] ([MultipleChoiceId], [OptionText1], [OptionText2], [OptionText3], [OptionText4], [CorrectChoice], [QuestionId]) VALUES (3, N'Camel', N'Giraffe', N'Banana', N'Walrus', 3, 2)
SET IDENTITY_INSERT [dbo].[MultipleChoices] OFF

SET IDENTITY_INSERT [dbo].[Locations] ON
INSERT INTO [dbo].[Locations] ([Campus], [Building], [Floor], [Block], [Room], [LocationId]) VALUES (N'TestCamp', N'jail', N'solid', N'chain', N'Test Room', 1)
SET IDENTITY_INSERT [dbo].[Locations] OFF

INSERT INTO [dbo].[ExamSessions] ([ExamId], [SubjectId], [LocationId], [ExamDateTime], [ExamEndTime], [QuestionAmount], [AccessCode], [CodeIssueDateTime], [FullyCorrected], [MaxMark], [MinMark], [AvgMark], [NumOfParticipants], [NumOfFails]) VALUES (N'TST2019-52019', N'TST2019', 1, NULL, NULL, 0, NULL, NULL, 0, 0, 0, 0, 0, 0)


");

        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExamSessions", "CodeIssueDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExamSessions", "ExamEndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExamSessions", "ExamDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.StudentAnswers", "CorrectedDateTime", c => c.DateTime(nullable: false));
        }
    }
}

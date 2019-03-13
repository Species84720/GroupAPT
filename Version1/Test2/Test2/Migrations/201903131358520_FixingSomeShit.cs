namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingSomeShit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MultipleChoices", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.ExamSessions", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.PaperQuestions", "QuestionId", "dbo.Questions");
            DropIndex("dbo.PaperQuestions", new[] { "QuestionId" });
            DropIndex("dbo.ExamSessions", new[] { "LocationId" });
            DropIndex("dbo.Questions", new[] { "TopicId" });
            DropIndex("dbo.MultipleChoices", new[] { "Question_QuestionId" });
            RenameColumn(table: "dbo.Enrollments", name: "UserId", newName: "StudentId");
            RenameIndex(table: "dbo.Enrollments", name: "IX_UserId", newName: "IX_StudentId");
            DropPrimaryKey("dbo.Locations");
            DropPrimaryKey("dbo.Questions");
            DropPrimaryKey("dbo.Topics");
            AlterColumn("dbo.PaperQuestions", "QuestionId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExamSessions", "LocationId", c => c.Int());
            AlterColumn("dbo.Locations", "LocationId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Questions", "QuestionId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Questions", "TopicId", c => c.Int());
            AlterColumn("dbo.Topics", "TopicId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Locations", "LocationId");
            AddPrimaryKey("dbo.Questions", "QuestionId");
            AddPrimaryKey("dbo.Topics", "TopicId");
            CreateIndex("dbo.PaperQuestions", "QuestionId");
            CreateIndex("dbo.ExamSessions", "LocationId");
            CreateIndex("dbo.Questions", "TopicId");
            AddForeignKey("dbo.Questions", "TopicId", "dbo.Topics", "TopicId");
            AddForeignKey("dbo.ExamSessions", "LocationId", "dbo.Locations", "LocationId");
            AddForeignKey("dbo.PaperQuestions", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
             
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MultipleChoices",
                c => new
                    {
                        ChoiceId = c.Int(nullable: false, identity: true),
                        OptionText1 = c.String(nullable: false),
                        OptionText2 = c.String(nullable: false),
                        OptionText3 = c.String(nullable: false),
                        OptionText4 = c.String(nullable: false),
                        CorrectChoice = c.Byte(nullable: false),
                        Question_QuestionId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ChoiceId);
            
            DropForeignKey("dbo.PaperQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.ExamSessions", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Questions", "TopicId", "dbo.Topics");
            DropIndex("dbo.Questions", new[] { "TopicId" });
            DropIndex("dbo.ExamSessions", new[] { "LocationId" });
            DropIndex("dbo.PaperQuestions", new[] { "QuestionId" });
            DropPrimaryKey("dbo.Topics");
            DropPrimaryKey("dbo.Questions");
            DropPrimaryKey("dbo.Locations");
            AlterColumn("dbo.Topics", "TopicId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Questions", "TopicId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Questions", "QuestionId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Locations", "LocationId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ExamSessions", "LocationId", c => c.String(maxLength: 128));
            AlterColumn("dbo.PaperQuestions", "QuestionId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Topics", "TopicId");
            AddPrimaryKey("dbo.Questions", "QuestionId");
            AddPrimaryKey("dbo.Locations", "LocationId");
            RenameIndex(table: "dbo.Enrollments", name: "IX_StudentId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Enrollments", name: "StudentId", newName: "UserId");
            CreateIndex("dbo.MultipleChoices", "Question_QuestionId");
            CreateIndex("dbo.Questions", "TopicId");
            CreateIndex("dbo.ExamSessions", "LocationId");
            CreateIndex("dbo.PaperQuestions", "QuestionId");
            AddForeignKey("dbo.PaperQuestions", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
            AddForeignKey("dbo.ExamSessions", "LocationId", "dbo.Locations", "LocationId");
            AddForeignKey("dbo.Questions", "TopicId", "dbo.Topics", "TopicId", cascadeDelete: true);
            AddForeignKey("dbo.MultipleChoices", "Question_QuestionId", "dbo.Questions", "QuestionId");
        }
    }
}

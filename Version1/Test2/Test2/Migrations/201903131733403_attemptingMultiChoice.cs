namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attemptingMultiChoice : DbMigration
    {
        public override void Up()
        {

            //Questions, studentAsnswers, Shots, Teachings, Topics

            //check syntax 
           // DropForeignKey("dbo.MultipleChoices", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.PaperQuestions", "QuestionId", "dbo.Questions");
            DropPrimaryKey("dbo.Questions");
            DropColumn("dbo.Questions", "QuestionId");
            AddColumn("dbo.Questions", "QuestionId", c => c.Int(nullable: true, identity: true));
            //AddForeignKey("dbo.MultipleChoices", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
            AddPrimaryKey("dbo.Questions", "QuestionId");
           
            AddForeignKey("dbo.PaperQuestions", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: false);

            //---
            DropPrimaryKey("dbo.StudentAnswers");
            DropColumn("dbo.StudentAnswers", "AnswerId");
            AddColumn("dbo.StudentAnswers", "AnswerId", c => c.Int(nullable: true, identity: true));

            AddPrimaryKey("dbo.StudentAnswers", "AnswerId");

            //---

            DropPrimaryKey("dbo.Shots");
            DropColumn("dbo.Shots", "ShotId");
            AddColumn("dbo.Shots", "ShotId", c => c.Int(nullable: true, identity: true));
            AddPrimaryKey("dbo.Shots", "ShotId");

            //---


            DropPrimaryKey("dbo.Teachings");
            DropColumn("dbo.Teachings", "TeachingId");
            AddColumn("dbo.Teachings", "TeachingId", c => c.Int(nullable: true, identity: true));
            AddPrimaryKey("dbo.Teachings", "TeachingId");

            //---

            DropForeignKey("dbo.Questions", "TopicId", "dbo.Topics");
            DropPrimaryKey("dbo.Topics");

            DropColumn("dbo.Topics", "TopicId");
            AddColumn("dbo.Topics", "TopicId", c => c.Int(nullable: true, identity: true));
            AddPrimaryKey("dbo.Topics", "TopicId");

            AddForeignKey("dbo.Questions", "TopicId", "dbo.Topics", "TopicId", cascadeDelete: false);

            //---


             
        }

        public override void Down()
        {
            DropForeignKey("dbo.MultipleChoices", "QuestionId", "dbo.Questions");
            DropIndex("dbo.MultipleChoices", new[] { "QuestionId" });
            DropTable("dbo.MultipleChoices");
        }
    }
}

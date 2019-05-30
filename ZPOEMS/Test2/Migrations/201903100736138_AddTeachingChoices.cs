namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeachingChoices : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.ChoiceId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .Index(t => t.Question_QuestionId);
            
            AddColumn("dbo.Questions", "QuestionFormat", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.MultipleChoices", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.MultipleChoices", new[] { "Question_QuestionId" });
            DropColumn("dbo.Questions", "QuestionFormat");
            DropTable("dbo.MultipleChoices");
        }
    }
}

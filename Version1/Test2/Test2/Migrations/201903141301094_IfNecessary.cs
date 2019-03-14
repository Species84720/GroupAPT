namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IfNecessary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MultipleChoices",
                c => new
                    {
                        MultipleChoiceId = c.Int(nullable: false, identity: true),
                        OptionText1 = c.String(nullable: false),
                        OptionText2 = c.String(nullable: false),
                        OptionText3 = c.String(nullable: false),
                        OptionText4 = c.String(nullable: false),
                        CorrectChoice = c.Byte(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MultipleChoiceId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MultipleChoices", "QuestionId", "dbo.Questions");
            DropIndex("dbo.MultipleChoices", new[] { "QuestionId" });
            DropTable("dbo.MultipleChoices");
        }
    }
}

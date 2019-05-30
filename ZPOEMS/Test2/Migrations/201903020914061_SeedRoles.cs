namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRoles : DbMigration
    {
        public override void Up()
        {
            Sql (@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Student')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Examiner')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Invigilator')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4', N'Clerk')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5', N'Admin')

INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b4cdbea0-4afb-4c20-b861-0ffac116ce6e', N'procompleter@gmail.com', 0, N'AFbjoQLgXSlhfgaPSIFjksFsS4OMKfSvqiqRi6HyqbqS3dy5HS5WExYzYOUcGo4OJA==', N'a151dcc4-cbb2-4a8b-97d1-de51539e8ac9', NULL, 0, 0, NULL, 1, 0, N'procompleter@gmail.com')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b4cdbea0-4afb-4c20-b861-0ffac116ce6e', N'5')


");
        }
        
        public override void Down()
        {
        }
    }
}

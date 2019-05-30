namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedMoreTestData : DbMigration
    {
        public override void Up()
        {

            Sql(@"
INSERT INTO [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'143b7e0b-dc92-46ee-913a-78a4a3db8ec8', N'1')

INSERT INTO [dbo].[AllUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [Surname], [DepartmentId], [NickName]) VALUES (N'7cf12b6b-845f-4005-9d15-66ede95a3412', N'tt@mail.com', 0, N'AF4bBPebZ/Ybzoc7dxjTfCEbYOzWuJRR4EJEW3FThcjyHyb33vwiT2Ag5fajsVNImg==', N'910609db-32b5-4c95-b1d7-8be3db7ea253', NULL, 0, 0, NULL, 1, 0, N'TerTea19', N'Terry', N'Teacher', 4, N'TerTea19')


INSERT INTO [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'7cf12b6b-845f-4005-9d15-66ede95a3412', N'2')

SET IDENTITY_INSERT [dbo].[Teachings] ON
INSERT INTO [dbo].[Teachings] ([ExaminerId], [SubjectId], [TeachingId]) VALUES (N'7cf12b6b-845f-4005-9d15-66ede95a3412', N'TST2019', 1)
SET IDENTITY_INSERT [dbo].[Teachings] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}

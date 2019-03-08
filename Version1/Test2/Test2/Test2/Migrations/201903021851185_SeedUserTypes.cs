namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUserTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@" SET IDENTITY_INSERT[dbo].[UserTypes]
        ON
INSERT INTO[dbo].[UserTypes]  ([ID], [UserRole]) VALUES(1, N'Student')
INSERT INTO[dbo].[UserTypes]  ([ID], [UserRole]) VALUES(2, N'Examiner')
INSERT INTO[dbo].[UserTypes]  ([ID], [UserRole]) VALUES(3, N'Invigilator')
INSERT INTO[dbo].[UserTypes]  ([ID], [UserRole]) VALUES(4, N'Clerk')
INSERT INTO[dbo].[UserTypes]  ([ID], [UserRole]) VALUES(5, N'Admin')
SET IDENTITY_INSERT[dbo].[UserTypes] OFF


");

        }
        
        public override void Down()
        {
        }
    }
}

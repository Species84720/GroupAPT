using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_APT.Migrations
{
    public partial class TotalDatabaseChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentUnitRelationships_Units_Code",
                table: "StudentUnitRelationships");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "StudentUnitRelationships",
                newName: "SubjectCode");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUnitRelationships_Students_UniversityStudentId",
                table: "StudentUnitRelationships");

            migrationBuilder.RenameColumn(
                name: "UniversityStudentId",
                table: "StudentUnitRelationships",
                newName: "UserId");

            //migrationBuilder.DropForeignKey(
            //name: "FK_Units_Department_DepartmentId",
            //table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Units",
                newName: "SubjectCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentUnitRelationships",
                table: "StudentUnitRelationships");

            migrationBuilder.RenameColumn(
                name: "RelationId",
                table: "StudentUnitRelationships",
                newName: "EnrollmentId");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "StudentUnitRelationships",
                newName: "Enrollments");

            //migrationBuilder.RenameIndex(
                //name: "IX_Units_DepartmentId",
                //table: "Subjects",
                //newName: "IX_Subjects_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUnitRelationships_UniversityStudentId",
                table: "Enrollments",
                newName: "IX_Enrollments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUnitRelationships_Code",
                table: "Enrollments",
                newName: "IX_Enrollments_SubjectCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "SubjectCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Subjects_SubjectCode",
                table: "Enrollments",
                column: "SubjectCode",
                principalTable: "Subjects",
                principalColumn: "SubjectCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(nullable: false),
                    DepartmentName = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Departments", x => x.DepartmentId); });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    UserType = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    DepartmentId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Department_DepartmentId",
                table: "Subjects",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Subjects_SubjectCode",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_UserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Department_DepartmentId",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "StudentUnitRelationships");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_DepartmentId",
                table: "Units",
                newName: "IX_Units_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_UserId",
                table: "StudentUnitRelationships",
                newName: "IX_StudentUnitRelationships_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_SubjectCode",
                table: "StudentUnitRelationships",
                newName: "IX_StudentUnitRelationships_SubjectCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "SubjectCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentUnitRelationships",
                table: "StudentUnitRelationships",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUnitRelationships_Units_SubjectCode",
                table: "StudentUnitRelationships",
                column: "SubjectCode",
                principalTable: "Units",
                principalColumn: "SubjectCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUnitRelationships_Students_UserId",
                table: "StudentUnitRelationships",
                column: "UserId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Department_DepartmentId",
                table: "Units",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_APT.Migrations
{
    public partial class Addition_Of_StudentUnit_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Lecturers");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Students",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UniversityStudentId",
                table: "Students",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LecturerId",
                table: "Lecturers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UniversityLecturerId",
                table: "Lecturers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "UniversityStudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers",
                column: "UniversityLecturerId");

            migrationBuilder.CreateTable(
                name: "StudentUnitRelationships",
                columns: table => new
                {
                    RelationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentRelationUniversityStudentId = table.Column<string>(nullable: true),
                    UnitRelationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUnitRelationships", x => x.RelationId);
                    table.ForeignKey(
                        name: "FK_StudentUnitRelationships_Students_StudentRelationUniversityStudentId",
                        column: x => x.StudentRelationUniversityStudentId,
                        principalTable: "Students",
                        principalColumn: "UniversityStudentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentUnitRelationships_Units_UnitRelationCode",
                        column: x => x.UnitRelationCode,
                        principalTable: "Units",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentUnitRelationships_StudentRelationUniversityStudentId",
                table: "StudentUnitRelationships",
                column: "StudentRelationUniversityStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUnitRelationships_UnitRelationCode",
                table: "StudentUnitRelationships",
                column: "UnitRelationCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentUnitRelationships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "UniversityStudentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UniversityLecturerId",
                table: "Lecturers");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Students",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LecturerId",
                table: "Lecturers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Lecturers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers",
                column: "LecturerId");
        }
    }
}

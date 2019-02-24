using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_APT.Migrations
{
    public partial class ChangeInForeignKeyStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentUnitRelationships_StudentUnit_RelationshipRelationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUnitRelationships_Students_StudentRelationUniversityStudentId",
                table: "StudentUnitRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUnitRelationships_Units_UnitRelationCode",
                table: "StudentUnitRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_StudentUnitRelationships_StudentUnit_RelationshipRelationId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_StudentUnit_RelationshipRelationId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentUnit_RelationshipRelationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentUnit_RelationshipRelationId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "StudentUnit_RelationshipRelationId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "UnitRelationCode",
                table: "StudentUnitRelationships",
                newName: "UniversityStudentId");

            migrationBuilder.RenameColumn(
                name: "StudentRelationUniversityStudentId",
                table: "StudentUnitRelationships",
                newName: "UnitCode");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUnitRelationships_UnitRelationCode",
                table: "StudentUnitRelationships",
                newName: "IX_StudentUnitRelationships_UniversityStudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUnitRelationships_StudentRelationUniversityStudentId",
                table: "StudentUnitRelationships",
                newName: "IX_StudentUnitRelationships_UnitCode");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "StudentUnitRelationships",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUnitRelationships_Units_UnitCode",
                table: "StudentUnitRelationships",
                column: "UnitCode",
                principalTable: "Units",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUnitRelationships_Students_UniversityStudentId",
                table: "StudentUnitRelationships",
                column: "UniversityStudentId",
                principalTable: "Students",
                principalColumn: "UniversityStudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentUnitRelationships_Units_UnitCode",
                table: "StudentUnitRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUnitRelationships_Students_UniversityStudentId",
                table: "StudentUnitRelationships");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentUnitRelationships");

            migrationBuilder.RenameColumn(
                name: "UniversityStudentId",
                table: "StudentUnitRelationships",
                newName: "UnitRelationCode");

            migrationBuilder.RenameColumn(
                name: "UnitCode",
                table: "StudentUnitRelationships",
                newName: "StudentRelationUniversityStudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUnitRelationships_UniversityStudentId",
                table: "StudentUnitRelationships",
                newName: "IX_StudentUnitRelationships_UnitRelationCode");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUnitRelationships_UnitCode",
                table: "StudentUnitRelationships",
                newName: "IX_StudentUnitRelationships_StudentRelationUniversityStudentId");

            migrationBuilder.AddColumn<int>(
                name: "StudentUnit_RelationshipRelationId",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentUnit_RelationshipRelationId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_StudentUnit_RelationshipRelationId",
                table: "Units",
                column: "StudentUnit_RelationshipRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentUnit_RelationshipRelationId",
                table: "Students",
                column: "StudentUnit_RelationshipRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentUnitRelationships_StudentUnit_RelationshipRelationId",
                table: "Students",
                column: "StudentUnit_RelationshipRelationId",
                principalTable: "StudentUnitRelationships",
                principalColumn: "RelationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUnitRelationships_Students_StudentRelationUniversityStudentId",
                table: "StudentUnitRelationships",
                column: "StudentRelationUniversityStudentId",
                principalTable: "Students",
                principalColumn: "UniversityStudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUnitRelationships_Units_UnitRelationCode",
                table: "StudentUnitRelationships",
                column: "UnitRelationCode",
                principalTable: "Units",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_StudentUnitRelationships_StudentUnit_RelationshipRelationId",
                table: "Units",
                column: "StudentUnit_RelationshipRelationId",
                principalTable: "StudentUnitRelationships",
                principalColumn: "RelationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

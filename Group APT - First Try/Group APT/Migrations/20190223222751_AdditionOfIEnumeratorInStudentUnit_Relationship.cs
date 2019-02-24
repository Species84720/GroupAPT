using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_APT.Migrations
{
    public partial class AdditionOfIEnumeratorInStudentUnit_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Units_StudentUnitRelationships_StudentUnit_RelationshipRelationId",
                table: "Units",
                column: "StudentUnit_RelationshipRelationId",
                principalTable: "StudentUnitRelationships",
                principalColumn: "RelationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentUnitRelationships_StudentUnit_RelationshipRelationId",
                table: "Students");

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
        }
    }
}

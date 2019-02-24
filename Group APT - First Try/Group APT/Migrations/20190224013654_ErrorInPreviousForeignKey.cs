using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_APT.Migrations
{
    public partial class ErrorInPreviousForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentUnitRelationships_Units_UnitCode",
                table: "StudentUnitRelationships");

            migrationBuilder.DropIndex(
                name: "IX_StudentUnitRelationships_UnitCode",
                table: "StudentUnitRelationships");

            migrationBuilder.AlterColumn<string>(
                name: "UnitCode",
                table: "StudentUnitRelationships",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "StudentUnitRelationships",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentUnitRelationships_Code",
                table: "StudentUnitRelationships",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUnitRelationships_Units_Code",
                table: "StudentUnitRelationships",
                column: "Code",
                principalTable: "Units",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentUnitRelationships_Units_Code",
                table: "StudentUnitRelationships");

            migrationBuilder.DropIndex(
                name: "IX_StudentUnitRelationships_Code",
                table: "StudentUnitRelationships");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "StudentUnitRelationships");

            migrationBuilder.AlterColumn<string>(
                name: "UnitCode",
                table: "StudentUnitRelationships",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentUnitRelationships_UnitCode",
                table: "StudentUnitRelationships",
                column: "UnitCode");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUnitRelationships_Units_UnitCode",
                table: "StudentUnitRelationships",
                column: "UnitCode",
                principalTable: "Units",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

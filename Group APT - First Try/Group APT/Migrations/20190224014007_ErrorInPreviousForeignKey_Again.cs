using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_APT.Migrations
{
    public partial class ErrorInPreviousForeignKey_Again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentUnitRelationships");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "StudentUnitRelationships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "StudentUnitRelationships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "StudentUnitRelationships",
                nullable: true);
        }
    }
}

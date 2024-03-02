using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeCBTSoftwareAccess.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrationGood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Taken",
                table: "StudentCourseReg",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taken",
                table: "StudentCourseReg");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaptchaVerificationSystem.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCorrectSelection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrectSelection",
                table: "CaptchaAttemptSelections",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrectSelection",
                table: "CaptchaAttemptSelections");
        }
    }
}

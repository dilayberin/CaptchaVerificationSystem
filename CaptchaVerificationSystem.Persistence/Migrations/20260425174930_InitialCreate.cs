using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaptchaVerificationSystem.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaptchaChallenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsSolved = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaptchaChallenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaptchaChallenges_Categories_TargetCategoryId",
                        column: x => x.TargetCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageCategories_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaptchaAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaptchaChallengeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttemptedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResponseTimeMs = table.Column<int>(type: "integer", nullable: false),
                    CorrectSelectionCount = table.Column<int>(type: "integer", nullable: false),
                    WrongSelectionCount = table.Column<int>(type: "integer", nullable: false),
                    MissedCorrectCount = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<decimal>(type: "numeric", nullable: false),
                    RiskLevel = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaptchaAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaptchaAttempts_CaptchaChallenges_CaptchaChallengeId",
                        column: x => x.CaptchaChallengeId,
                        principalTable: "CaptchaChallenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaptchaChallengeImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaptchaChallengeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaptchaChallengeImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaptchaChallengeImages_CaptchaChallenges_CaptchaChallengeId",
                        column: x => x.CaptchaChallengeId,
                        principalTable: "CaptchaChallenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaptchaChallengeImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaptchaAttemptSelections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaptchaAttemptId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaptchaChallengeImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsSelected = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaptchaAttemptSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaptchaAttemptSelections_CaptchaAttempts_CaptchaAttemptId",
                        column: x => x.CaptchaAttemptId,
                        principalTable: "CaptchaAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaptchaAttemptSelections_CaptchaChallengeImages_CaptchaChal~",
                        column: x => x.CaptchaChallengeImageId,
                        principalTable: "CaptchaChallengeImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaptchaAttempts_CaptchaChallengeId",
                table: "CaptchaAttempts",
                column: "CaptchaChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_CaptchaAttemptSelections_CaptchaAttemptId",
                table: "CaptchaAttemptSelections",
                column: "CaptchaAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_CaptchaAttemptSelections_CaptchaChallengeImageId",
                table: "CaptchaAttemptSelections",
                column: "CaptchaChallengeImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CaptchaChallengeImages_CaptchaChallengeId",
                table: "CaptchaChallengeImages",
                column: "CaptchaChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_CaptchaChallengeImages_ImageId",
                table: "CaptchaChallengeImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CaptchaChallenges_TargetCategoryId",
                table: "CaptchaChallenges",
                column: "TargetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageCategories_CategoryId",
                table: "ImageCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageCategories_ImageId",
                table: "ImageCategories",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaptchaAttemptSelections");

            migrationBuilder.DropTable(
                name: "ImageCategories");

            migrationBuilder.DropTable(
                name: "CaptchaAttempts");

            migrationBuilder.DropTable(
                name: "CaptchaChallengeImages");

            migrationBuilder.DropTable(
                name: "CaptchaChallenges");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

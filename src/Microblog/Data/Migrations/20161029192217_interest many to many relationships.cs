using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microblog.Data.Migrations
{
    public partial class interestmanytomanyrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DropForeignKey(
                name: "FK_Interest_AspNetUsers_ApplicationUserId",
                table: "Interest");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Post_PostID",
                table: "Interest");

            migrationBuilder.DropIndex(
                name: "IX_Interest_ApplicationUserId",
                table: "Interest");

            migrationBuilder.DropIndex(
                name: "IX_Interest_PostID",
                table: "Interest");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Interest");

            migrationBuilder.DropColumn(
                name: "PostID",
                table: "Interest"); */

            migrationBuilder.CreateTable(
                name: "PostInterests",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    InterestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInterests", x => new { x.PostId, x.InterestId });
                    table.ForeignKey(
                        name: "FK_PostInterests_Interest_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostInterests_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInterests",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    InterestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterests", x => new { x.ApplicationUserId, x.InterestId });
                    table.ForeignKey(
                        name: "FK_UserInterests_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInterests_Interest_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostInterests_InterestId",
                table: "PostInterests",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInterests_PostId",
                table: "PostInterests",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_ApplicationUserId",
                table: "UserInterests",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_InterestId",
                table: "UserInterests",
                column: "InterestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostInterests");

            migrationBuilder.DropTable(
                name: "UserInterests");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Interest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostID",
                table: "Interest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interest_ApplicationUserId",
                table: "Interest",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_PostID",
                table: "Interest",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_AspNetUsers_ApplicationUserId",
                table: "Interest",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Post_PostID",
                table: "Interest",
                column: "PostID",
                principalTable: "Post",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

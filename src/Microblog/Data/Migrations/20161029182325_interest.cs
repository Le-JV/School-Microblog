using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microblog.Data.Migrations
{
    public partial class interest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_postID",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "PostID",
                table: "Interest",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Interest_PostID",
                table: "Interest",
                column: "PostID");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comment",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostID",
                table: "Comment",
                column: "postID",
                principalTable: "Post",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Post_PostID",
                table: "Interest",
                column: "PostID",
                principalTable: "Post",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "postID",
                table: "Comment",
                newName: "PostID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_postID",
                table: "Comment",
                newName: "IX_Comment_PostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostID",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Post_PostID",
                table: "Interest");

            migrationBuilder.DropIndex(
                name: "IX_Interest_PostID",
                table: "Interest");

            migrationBuilder.DropColumn(
                name: "PostID",
                table: "Interest");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comment",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_postID",
                table: "Comment",
                column: "PostID",
                principalTable: "Post",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "PostID",
                table: "Comment",
                newName: "postID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostID",
                table: "Comment",
                newName: "IX_Comment_postID");
        }
    }
}

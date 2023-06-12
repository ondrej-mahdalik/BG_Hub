using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrokenGrenade.Web.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleEntities_ArticleCategoryEntities_CategoryId",
                table: "ArticleEntities");

            migrationBuilder.DropTable(
                name: "ArticleCategoryEntities");

            migrationBuilder.DropIndex(
                name: "IX_ArticleEntities_CategoryId",
                table: "ArticleEntities");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ArticleEntities",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ArticleEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "ArticleEntities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Excerpt",
                table: "ArticleEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEntities_CreatorId",
                table: "ArticleEntities",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleEntities_AspNetUsers_CreatorId",
                table: "ArticleEntities",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleEntities_AspNetUsers_CreatorId",
                table: "ArticleEntities");

            migrationBuilder.DropIndex(
                name: "IX_ArticleEntities_CreatorId",
                table: "ArticleEntities");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "ArticleEntities");

            migrationBuilder.DropColumn(
                name: "Excerpt",
                table: "ArticleEntities");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ArticleEntities",
                newName: "CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ArticleEntities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "ArticleCategoryEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategoryEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleCategoryEntities_ArticleCategoryEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ArticleCategoryEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEntities_CategoryId",
                table: "ArticleEntities",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCategoryEntities_ParentId",
                table: "ArticleCategoryEntities",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleEntities_ArticleCategoryEntities_CategoryId",
                table: "ArticleEntities",
                column: "CategoryId",
                principalTable: "ArticleCategoryEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

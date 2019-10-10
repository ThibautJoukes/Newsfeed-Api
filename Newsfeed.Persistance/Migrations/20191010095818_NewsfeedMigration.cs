using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Newsfeed.Persistance.Migrations
{
    public partial class NewsfeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsfeedArticleSource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSource = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsfeedArticleSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsfeedArticle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentSourceId = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UrlToImage = table.Column<string>(nullable: true),
                    PublishedAt = table.Column<DateTimeOffset>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsfeedArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsfeedArticle_NewsfeedArticleSource_CurrentSourceId",
                        column: x => x.CurrentSourceId,
                        principalTable: "NewsfeedArticleSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsfeedArticle_CurrentSourceId",
                table: "NewsfeedArticle",
                column: "CurrentSourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsfeedArticle");

            migrationBuilder.DropTable(
                name: "NewsfeedArticleSource");
        }
    }
}

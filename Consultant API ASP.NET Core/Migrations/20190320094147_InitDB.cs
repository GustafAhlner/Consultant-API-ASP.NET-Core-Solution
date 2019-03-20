using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Consultant_API_ASP.NET_Core.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "competences",
                columns: table => new
                {
                    CompetenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetenceName = table.Column<string>(nullable: true),
                    CompetenceLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_competences", x => x.CompetenceId);
                });

            migrationBuilder.CreateTable(
                name: "consultants",
                columns: table => new
                {
                    ConsultantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    NameFirst = table.Column<string>(nullable: true),
                    NameSecond = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consultants", x => x.ConsultantId);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLine = table.Column<string>(nullable: true),
                    PostalCode = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    CountryRegion = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    ConsultantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_addresses_consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "consultants",
                        principalColumn: "ConsultantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultantCompetences",
                columns: table => new
                {
                    ConsultantId = table.Column<int>(nullable: false),
                    CompetenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantCompetences", x => new { x.CompetenceId, x.ConsultantId });
                    table.ForeignKey(
                        name: "FK_ConsultantCompetences_competences_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "competences",
                        principalColumn: "CompetenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultantCompetences_consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "consultants",
                        principalColumn: "ConsultantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_ConsultantId",
                table: "addresses",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantCompetences_ConsultantId",
                table: "ConsultantCompetences",
                column: "ConsultantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "ConsultantCompetences");

            migrationBuilder.DropTable(
                name: "competences");

            migrationBuilder.DropTable(
                name: "consultants");
        }
    }
}

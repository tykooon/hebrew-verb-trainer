using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HebrewVerb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GizraShoresh");

            migrationBuilder.DropTable(
                name: "Gizras");

            migrationBuilder.RenameColumn(
                name: "IsRare",
                table: "Verbs",
                newName: "UseFrequency");

            migrationBuilder.AddColumn<bool>(
                name: "IsSlang",
                table: "Verbs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "VerbModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsGizra = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerbModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoreshVerbModel",
                columns: table => new
                {
                    ShoreshesId = table.Column<int>(type: "INTEGER", nullable: false),
                    VerbModelsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoreshVerbModel", x => new { x.ShoreshesId, x.VerbModelsId });
                    table.ForeignKey(
                        name: "FK_ShoreshVerbModel_Shoreshes_ShoreshesId",
                        column: x => x.ShoreshesId,
                        principalTable: "Shoreshes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoreshVerbModel_VerbModels_VerbModelsId",
                        column: x => x.VerbModelsId,
                        principalTable: "VerbModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoreshVerbModel_VerbModelsId",
                table: "ShoreshVerbModel",
                column: "VerbModelsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoreshVerbModel");

            migrationBuilder.DropTable(
                name: "VerbModels");

            migrationBuilder.DropColumn(
                name: "IsSlang",
                table: "Verbs");

            migrationBuilder.RenameColumn(
                name: "UseFrequency",
                table: "Verbs",
                newName: "IsRare");

            migrationBuilder.CreateTable(
                name: "Gizras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gizras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GizraShoresh",
                columns: table => new
                {
                    GizrasId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShoreshesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GizraShoresh", x => new { x.GizrasId, x.ShoreshesId });
                    table.ForeignKey(
                        name: "FK_GizraShoresh_Gizras_GizrasId",
                        column: x => x.GizrasId,
                        principalTable: "Gizras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GizraShoresh_Shoreshes_ShoreshesId",
                        column: x => x.ShoreshesId,
                        principalTable: "Shoreshes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GizraShoresh_ShoreshesId",
                table: "GizraShoresh",
                column: "ShoreshesId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HebrewVerb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gizras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gizras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shoreshes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoreshes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Verbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Inf = table.Column<string>(type: "TEXT", nullable: true),
                    InfT = table.Column<string>(type: "TEXT", nullable: true),
                    Binyan = table.Column<int>(type: "INTEGER", nullable: false),
                    ShoreshId = table.Column<int>(type: "INTEGER", nullable: true),
                    PreMS = table.Column<string>(type: "TEXT", nullable: true),
                    PreFS = table.Column<string>(type: "TEXT", nullable: true),
                    PreMP = table.Column<string>(type: "TEXT", nullable: true),
                    PreFP = table.Column<string>(type: "TEXT", nullable: true),
                    Pas1S = table.Column<string>(type: "TEXT", nullable: true),
                    Pas1P = table.Column<string>(type: "TEXT", nullable: true),
                    Pas2MS = table.Column<string>(type: "TEXT", nullable: true),
                    Pas2FS = table.Column<string>(type: "TEXT", nullable: true),
                    Pas2MP = table.Column<string>(type: "TEXT", nullable: true),
                    Pas2FP = table.Column<string>(type: "TEXT", nullable: true),
                    Pas3MS = table.Column<string>(type: "TEXT", nullable: true),
                    Pas3FS = table.Column<string>(type: "TEXT", nullable: true),
                    Pas3P = table.Column<string>(type: "TEXT", nullable: true),
                    Fut1S = table.Column<string>(type: "TEXT", nullable: true),
                    Fut1P = table.Column<string>(type: "TEXT", nullable: true),
                    Fut2MS = table.Column<string>(type: "TEXT", nullable: true),
                    Fut2FS = table.Column<string>(type: "TEXT", nullable: true),
                    Fut2P = table.Column<string>(type: "TEXT", nullable: true),
                    Fut3MS = table.Column<string>(type: "TEXT", nullable: true),
                    Fut3FS = table.Column<string>(type: "TEXT", nullable: true),
                    Fut3P = table.Column<string>(type: "TEXT", nullable: true),
                    Imp2MS = table.Column<string>(type: "TEXT", nullable: true),
                    Imp2FS = table.Column<string>(type: "TEXT", nullable: true),
                    Imp2P = table.Column<string>(type: "TEXT", nullable: true),
                    PreMST = table.Column<string>(type: "TEXT", nullable: true),
                    PreFST = table.Column<string>(type: "TEXT", nullable: true),
                    PreMPT = table.Column<string>(type: "TEXT", nullable: true),
                    PreFPT = table.Column<string>(type: "TEXT", nullable: true),
                    Pas1ST = table.Column<string>(type: "TEXT", nullable: true),
                    Pas1PT = table.Column<string>(type: "TEXT", nullable: true),
                    Pas2MST = table.Column<string>(type: "TEXT", nullable: true),
                    Pas2FST = table.Column<string>(type: "TEXT", nullable: true),
                    Pas2MPT = table.Column<string>(type: "TEXT", nullable: true),
                    Pas2FPT = table.Column<string>(type: "TEXT", nullable: true),
                    Pas3MST = table.Column<string>(type: "TEXT", nullable: true),
                    Pas3FST = table.Column<string>(type: "TEXT", nullable: true),
                    Pas3PT = table.Column<string>(type: "TEXT", nullable: true),
                    Fut1ST = table.Column<string>(type: "TEXT", nullable: true),
                    Fut1PT = table.Column<string>(type: "TEXT", nullable: true),
                    Fut2MST = table.Column<string>(type: "TEXT", nullable: true),
                    Fut2FST = table.Column<string>(type: "TEXT", nullable: true),
                    Fut2PT = table.Column<string>(type: "TEXT", nullable: true),
                    Fut3MST = table.Column<string>(type: "TEXT", nullable: true),
                    Fut3FST = table.Column<string>(type: "TEXT", nullable: true),
                    Fut3PT = table.Column<string>(type: "TEXT", nullable: true),
                    Imp2MST = table.Column<string>(type: "TEXT", nullable: true),
                    Imp2FST = table.Column<string>(type: "TEXT", nullable: true),
                    Imp2PT = table.Column<string>(type: "TEXT", nullable: true),
                    Translate = table.Column<string>(type: "TEXT", nullable: true),
                    IsArchaic = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLiterary = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRare = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verbs_Shoreshes_ShoreshId",
                        column: x => x.ShoreshId,
                        principalTable: "Shoreshes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GizraShoresh_ShoreshesId",
                table: "GizraShoresh",
                column: "ShoreshesId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_ShoreshId",
                table: "Verbs",
                column: "ShoreshId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GizraShoresh");

            migrationBuilder.DropTable(
                name: "Verbs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Gizras");

            migrationBuilder.DropTable(
                name: "Shoreshes");
        }
    }
}

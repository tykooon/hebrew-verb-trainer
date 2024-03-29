﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HebrewVerb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
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
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "Shoreshes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Short = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoreshes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hebrew = table.Column<string>(type: "TEXT", nullable: false),
                    HebrewNiqqud = table.Column<string>(type: "TEXT", nullable: false),
                    TranscriptionRus = table.Column<string>(type: "TEXT", nullable: true),
                    StressLetterRus = table.Column<int>(type: "INTEGER", nullable: false),
                    TranscriptionEng = table.Column<string>(type: "TEXT", nullable: true),
                    StressLetterEng = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordForms", x => x.Id);
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
                name: "AppUserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserSettings_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
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
                name: "FilterSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Filter = table.Column<string>(type: "TEXT", nullable: false),
                    FilterName = table.Column<string>(type: "TEXT", nullable: false),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterSnapshots_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Futures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MS1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MS2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FS2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MS3Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP3Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Futures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Futures_WordForms_FS2Id",
                        column: x => x.FS2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Futures_WordForms_MP1Id",
                        column: x => x.MP1Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Futures_WordForms_MP2Id",
                        column: x => x.MP2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Futures_WordForms_MP3Id",
                        column: x => x.MP3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Futures_WordForms_MS1Id",
                        column: x => x.MS1Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Futures_WordForms_MS2Id",
                        column: x => x.MS2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Futures_WordForms_MS3Id",
                        column: x => x.MS3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imperatives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MSId = table.Column<int>(type: "INTEGER", nullable: true),
                    FSId = table.Column<int>(type: "INTEGER", nullable: true),
                    MPId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imperatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imperatives_WordForms_FSId",
                        column: x => x.FSId,
                        principalTable: "WordForms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Imperatives_WordForms_MPId",
                        column: x => x.MPId,
                        principalTable: "WordForms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Imperatives_WordForms_MSId",
                        column: x => x.MSId,
                        principalTable: "WordForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MS1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MS2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FS2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FP2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MS3Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FS3Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP3Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_FP2Id",
                        column: x => x.FP2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_FS2Id",
                        column: x => x.FS2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_FS3Id",
                        column: x => x.FS3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_MP1Id",
                        column: x => x.MP1Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_MP2Id",
                        column: x => x.MP2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_MP3Id",
                        column: x => x.MP3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_MS1Id",
                        column: x => x.MS1Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_MS2Id",
                        column: x => x.MS2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasts_WordForms_MS3Id",
                        column: x => x.MS3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prepositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BaseFormId = table.Column<int>(type: "INTEGER", nullable: false),
                    TranslateRus = table.Column<string>(type: "TEXT", nullable: false),
                    TranslateEng = table.Column<string>(type: "TEXT", nullable: false),
                    MS1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MS2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FS2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FP2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MS3Id = table.Column<int>(type: "INTEGER", nullable: false),
                    MP3Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FS3Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FP3Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prepositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_BaseFormId",
                        column: x => x.BaseFormId,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_FP2Id",
                        column: x => x.FP2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_FP3Id",
                        column: x => x.FP3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_FS2Id",
                        column: x => x.FS2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_FS3Id",
                        column: x => x.FS3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_MP1Id",
                        column: x => x.MP1Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_MP2Id",
                        column: x => x.MP2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_MP3Id",
                        column: x => x.MP3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_MS1Id",
                        column: x => x.MS1Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_MS2Id",
                        column: x => x.MS2Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prepositions_WordForms_MS3Id",
                        column: x => x.MS3Id,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Presents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MSId = table.Column<int>(type: "INTEGER", nullable: false),
                    FSId = table.Column<int>(type: "INTEGER", nullable: false),
                    MPId = table.Column<int>(type: "INTEGER", nullable: false),
                    FPId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presents_WordForms_FPId",
                        column: x => x.FPId,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presents_WordForms_FSId",
                        column: x => x.FSId,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presents_WordForms_MPId",
                        column: x => x.MPId,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presents_WordForms_MSId",
                        column: x => x.MSId,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InfinitiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Binyan = table.Column<int>(type: "INTEGER", nullable: false),
                    ShoreshId = table.Column<int>(type: "INTEGER", nullable: false),
                    PresentId = table.Column<int>(type: "INTEGER", nullable: true),
                    PastId = table.Column<int>(type: "INTEGER", nullable: true),
                    FutureId = table.Column<int>(type: "INTEGER", nullable: true),
                    ImperativeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tags = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraInfo = table.Column<string>(type: "TEXT", nullable: false),
                    VerbModels = table.Column<int>(type: "INTEGER", nullable: false),
                    Gizras = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verbs_Futures_FutureId",
                        column: x => x.FutureId,
                        principalTable: "Futures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Verbs_Imperatives_ImperativeId",
                        column: x => x.ImperativeId,
                        principalTable: "Imperatives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Verbs_Pasts_PastId",
                        column: x => x.PastId,
                        principalTable: "Pasts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Verbs_Presents_PresentId",
                        column: x => x.PresentId,
                        principalTable: "Presents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Verbs_Shoreshes_ShoreshId",
                        column: x => x.ShoreshId,
                        principalTable: "Shoreshes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verbs_WordForms_InfinitiveId",
                        column: x => x.InfinitiveId,
                        principalTable: "WordForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<int>(type: "INTEGER", nullable: false),
                    Main = table.Column<string>(type: "TEXT", nullable: false),
                    Auxillare = table.Column<string>(type: "TEXT", nullable: false),
                    Tags = table.Column<int>(type: "INTEGER", nullable: false),
                    VerbId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_Verbs_VerbId",
                        column: x => x.VerbId,
                        principalTable: "Verbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepositionTranslation",
                columns: table => new
                {
                    PrepositionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TranslationsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepositionTranslation", x => new { x.PrepositionsId, x.TranslationsId });
                    table.ForeignKey(
                        name: "FK_PrepositionTranslation_Prepositions_PrepositionsId",
                        column: x => x.PrepositionsId,
                        principalTable: "Prepositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrepositionTranslation_Translations_TranslationsId",
                        column: x => x.TranslationsId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSettings_AppUserId",
                table: "AppUserSettings",
                column: "AppUserId",
                unique: true);

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
                name: "IX_FilterSnapshots_AppUserId",
                table: "FilterSnapshots",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Futures_FS2Id",
                table: "Futures",
                column: "FS2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Futures_MP1Id",
                table: "Futures",
                column: "MP1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Futures_MP2Id",
                table: "Futures",
                column: "MP2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Futures_MP3Id",
                table: "Futures",
                column: "MP3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Futures_MS1Id",
                table: "Futures",
                column: "MS1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Futures_MS2Id",
                table: "Futures",
                column: "MS2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Futures_MS3Id",
                table: "Futures",
                column: "MS3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Imperatives_FSId",
                table: "Imperatives",
                column: "FSId");

            migrationBuilder.CreateIndex(
                name: "IX_Imperatives_MPId",
                table: "Imperatives",
                column: "MPId");

            migrationBuilder.CreateIndex(
                name: "IX_Imperatives_MSId",
                table: "Imperatives",
                column: "MSId");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_FP2Id",
                table: "Pasts",
                column: "FP2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_FS2Id",
                table: "Pasts",
                column: "FS2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_FS3Id",
                table: "Pasts",
                column: "FS3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_MP1Id",
                table: "Pasts",
                column: "MP1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_MP2Id",
                table: "Pasts",
                column: "MP2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_MP3Id",
                table: "Pasts",
                column: "MP3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_MS1Id",
                table: "Pasts",
                column: "MS1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_MS2Id",
                table: "Pasts",
                column: "MS2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_MS3Id",
                table: "Pasts",
                column: "MS3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_BaseFormId",
                table: "Prepositions",
                column: "BaseFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_FP2Id",
                table: "Prepositions",
                column: "FP2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_FP3Id",
                table: "Prepositions",
                column: "FP3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_FS2Id",
                table: "Prepositions",
                column: "FS2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_FS3Id",
                table: "Prepositions",
                column: "FS3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_MP1Id",
                table: "Prepositions",
                column: "MP1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_MP2Id",
                table: "Prepositions",
                column: "MP2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_MP3Id",
                table: "Prepositions",
                column: "MP3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_MS1Id",
                table: "Prepositions",
                column: "MS1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_MS2Id",
                table: "Prepositions",
                column: "MS2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prepositions_MS3Id",
                table: "Prepositions",
                column: "MS3Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrepositionTranslation_TranslationsId",
                table: "PrepositionTranslation",
                column: "TranslationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Presents_FPId",
                table: "Presents",
                column: "FPId");

            migrationBuilder.CreateIndex(
                name: "IX_Presents_FSId",
                table: "Presents",
                column: "FSId");

            migrationBuilder.CreateIndex(
                name: "IX_Presents_MPId",
                table: "Presents",
                column: "MPId");

            migrationBuilder.CreateIndex(
                name: "IX_Presents_MSId",
                table: "Presents",
                column: "MSId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoreshes_Short",
                table: "Shoreshes",
                column: "Short",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translations_VerbId",
                table: "Translations",
                column: "VerbId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_FutureId",
                table: "Verbs",
                column: "FutureId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_ImperativeId",
                table: "Verbs",
                column: "ImperativeId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_InfinitiveId",
                table: "Verbs",
                column: "InfinitiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_PastId",
                table: "Verbs",
                column: "PastId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_PresentId",
                table: "Verbs",
                column: "PresentId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_ShoreshId",
                table: "Verbs",
                column: "ShoreshId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserSettings");

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
                name: "FilterSnapshots");

            migrationBuilder.DropTable(
                name: "PrepositionTranslation");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Prepositions");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Verbs");

            migrationBuilder.DropTable(
                name: "Futures");

            migrationBuilder.DropTable(
                name: "Imperatives");

            migrationBuilder.DropTable(
                name: "Pasts");

            migrationBuilder.DropTable(
                name: "Presents");

            migrationBuilder.DropTable(
                name: "Shoreshes");

            migrationBuilder.DropTable(
                name: "WordForms");
        }
    }
}

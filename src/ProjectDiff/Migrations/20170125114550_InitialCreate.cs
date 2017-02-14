using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectDiff.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    SolutionID = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.SolutionID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    SolutionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Projects_Solutions_SolutionID",
                        column: x => x.SolutionID,
                        principalTable: "Solutions",
                        principalColumn: "SolutionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Snapshots",
                columns: table => new
                {
                    SnapshotID = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    SolutionID = table.Column<long>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snapshots", x => x.SnapshotID);
                    table.ForeignKey(
                        name: "FK_Snapshots_Solutions_SolutionID",
                        column: x => x.SolutionID,
                        principalTable: "Solutions",
                        principalColumn: "SolutionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileProperties",
                columns: table => new
                {
                    FilePropertyID = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    ProjectID = table.Column<long>(nullable: false),
                    TfsPath = table.Column<string>(nullable: true),
                    checksum = table.Column<string>(nullable: true),
                    path = table.Column<string>(nullable: true),
                    project = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileProperties", x => x.FilePropertyID);
                    table.ForeignKey(
                        name: "FK_FileProperties_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SnapshotProjects",
                columns: table => new
                {
                    SnapshotID = table.Column<long>(nullable: false),
                    ProjectID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnapshotProjects", x => new { x.SnapshotID, x.ProjectID });
                    table.ForeignKey(
                        name: "FK_SnapshotProjects_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnapshotProjects_Snapshots_SnapshotID",
                        column: x => x.SnapshotID,
                        principalTable: "Snapshots",
                        principalColumn: "SnapshotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SnapshotFileProperties",
                columns: table => new
                {
                    SnapshotID = table.Column<long>(nullable: false),
                    FilePropertyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnapshotFileProperties", x => new { x.SnapshotID, x.FilePropertyID });
                    table.ForeignKey(
                        name: "FK_SnapshotFileProperties_FileProperties_FilePropertyID",
                        column: x => x.FilePropertyID,
                        principalTable: "FileProperties",
                        principalColumn: "FilePropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnapshotFileProperties_Snapshots_SnapshotID",
                        column: x => x.SnapshotID,
                        principalTable: "Snapshots",
                        principalColumn: "SnapshotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileProperties_ProjectID",
                table: "FileProperties",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SolutionID",
                table: "Projects",
                column: "SolutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Snapshots_SolutionID",
                table: "Snapshots",
                column: "SolutionID");

            migrationBuilder.CreateIndex(
                name: "IX_SnapshotFileProperties_FilePropertyID",
                table: "SnapshotFileProperties",
                column: "FilePropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_SnapshotFileProperties_SnapshotID",
                table: "SnapshotFileProperties",
                column: "SnapshotID");

            migrationBuilder.CreateIndex(
                name: "IX_SnapshotProjects_ProjectID",
                table: "SnapshotProjects",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SnapshotProjects_SnapshotID",
                table: "SnapshotProjects",
                column: "SnapshotID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnapshotFileProperties");

            migrationBuilder.DropTable(
                name: "SnapshotProjects");

            migrationBuilder.DropTable(
                name: "FileProperties");

            migrationBuilder.DropTable(
                name: "Snapshots");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Solutions");
        }
    }
}

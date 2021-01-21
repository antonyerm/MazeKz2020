using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMaze.Migrations
{
    public partial class LifeInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accident",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccidentAddressId = table.Column<long>(type: "bigint", nullable: true),
                    AccidentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccidentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accident", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accident_Adress_AccidentAddressId",
                        column: x => x.AccidentAddressId,
                        principalTable: "Adress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccidentVictim",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodilyHarm = table.Column<int>(type: "int", nullable: true),
                    EconomicLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VictimId = table.Column<long>(type: "bigint", nullable: true),
                    AccidentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentVictim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccidentVictim_Accident_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccidentVictim_CitizenUser_VictimId",
                        column: x => x.VictimId,
                        principalTable: "CitizenUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CriminalOffenceDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OffenceArticle = table.Column<int>(type: "int", nullable: false),
                    AccidentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriminalOffenceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriminalOffenceDetail_Accident_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CriminalOffender",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Verdict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OffenderId = table.Column<long>(type: "bigint", nullable: true),
                    AccidentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriminalOffender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriminalOffender_Accident_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CriminalOffender_CitizenUser_OffenderId",
                        column: x => x.OffenderId,
                        principalTable: "CitizenUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FireDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FireCause = table.Column<int>(type: "int", nullable: true),
                    FireClass = table.Column<int>(type: "int", nullable: true),
                    AccidentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireDetail_Accident_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseDestroyedInFire",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccidentId = table.Column<long>(type: "bigint", nullable: false),
                    DestroyedHouseAddressId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseDestroyedInFire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseDestroyedInFire_Accident_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseDestroyedInFire_Adress_DestroyedHouseAddressId",
                        column: x => x.DestroyedHouseAddressId,
                        principalTable: "Adress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accident_AccidentAddressId",
                table: "Accident",
                column: "AccidentAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AccidentVictim_AccidentId",
                table: "AccidentVictim",
                column: "AccidentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccidentVictim_VictimId",
                table: "AccidentVictim",
                column: "VictimId");

            migrationBuilder.CreateIndex(
                name: "IX_CriminalOffenceDetail_AccidentId",
                table: "CriminalOffenceDetail",
                column: "AccidentId");

            migrationBuilder.CreateIndex(
                name: "IX_CriminalOffender_AccidentId",
                table: "CriminalOffender",
                column: "AccidentId");

            migrationBuilder.CreateIndex(
                name: "IX_CriminalOffender_OffenderId",
                table: "CriminalOffender",
                column: "OffenderId");

            migrationBuilder.CreateIndex(
                name: "IX_FireDetail_AccidentId",
                table: "FireDetail",
                column: "AccidentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HouseDestroyedInFire_AccidentId",
                table: "HouseDestroyedInFire",
                column: "AccidentId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseDestroyedInFire_DestroyedHouseAddressId",
                table: "HouseDestroyedInFire",
                column: "DestroyedHouseAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentVictim");

            migrationBuilder.DropTable(
                name: "CriminalOffenceDetail");

            migrationBuilder.DropTable(
                name: "CriminalOffender");

            migrationBuilder.DropTable(
                name: "FireDetail");

            migrationBuilder.DropTable(
                name: "HouseDestroyedInFire");

            migrationBuilder.DropTable(
                name: "Accident");
        }
    }
}

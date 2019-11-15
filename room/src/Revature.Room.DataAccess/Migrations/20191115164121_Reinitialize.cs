using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Revature.Room.DataAccess.Migrations
{
    public partial class Reinitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomID = table.Column<Guid>(nullable: false),
                    RoomNumber = table.Column<string>(nullable: false),
                    NumberOfBeds = table.Column<int>(nullable: false),
                    GenderId = table.Column<Guid>(nullable: false),
                    RoomTypeId = table.Column<Guid>(nullable: false),
                    LeaseStart = table.Column<DateTime>(nullable: false),
                    LeaseEnd = table.Column<DateTime>(nullable: false),
                    ComplexID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Room_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_RoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("01743da3-37c2-47f5-b545-9abcea22f7a3"), "Male" },
                    { new Guid("7f390145-656c-4e2b-82d7-68ec80467ed9"), "Female" },
                    { new Guid("e805c884-07c9-475d-9384-a67da9a3d1f6"), "Nonbinary" }
                });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("95016bcc-f34b-421c-978d-b10fcac91841"), "Apartment" },
                    { new Guid("01fd727b-b826-4f33-943f-00a5e3729197"), "Dormitory" },
                    { new Guid("3dbe5495-af68-4c1d-847f-2f30a859ba93"), "Townhouse" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_GenderId",
                table: "Room",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomTypeId",
                table: "Room",
                column: "RoomTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "RoomType");
        }
    }
}

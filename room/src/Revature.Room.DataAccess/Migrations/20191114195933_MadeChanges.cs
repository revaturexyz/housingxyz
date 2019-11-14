using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Revature.Room.DataAccess.Migrations
{
    public partial class MadeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomID", "ComplexID", "Gender", "LeaseEnd", "LeaseStart", "NumberOfBeds", "RoomNumber", "RoomType" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2019, 11, 14, 16, 59, 33, 250, DateTimeKind.Local).AddTicks(756), 4, "2428B", 0 },
                    { 2, 1, 0, new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2019, 11, 14, 16, 59, 33, 250, DateTimeKind.Local).AddTicks(1503), 4, "221B", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "RoomID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "RoomID",
                keyValue: 2);
        }
    }
}

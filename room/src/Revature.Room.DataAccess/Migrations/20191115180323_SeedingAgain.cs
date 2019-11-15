using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Revature.Room.DataAccess.Migrations
{
    public partial class SeedingAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "RoomID",
                keyValue: new Guid("4cb45dcc-1426-43c2-b331-368afbfd934b"));

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomID", "ComplexID", "GenderType", "LeaseEnd", "LeaseStart", "NumberOfBeds", "RoomNumber", "RoomTypeType" },
                values: new object[] { new Guid("249e5358-169a-4bc6-aa0f-c054952456fd"), new Guid("b5e050aa-6bfc-46ad-9a69-90b1f99ed606"), "Female", new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2019, 11, 15, 12, 3, 23, 65, DateTimeKind.Local).AddTicks(256), 4, "2428B", "Apartment" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "RoomID",
                keyValue: new Guid("249e5358-169a-4bc6-aa0f-c054952456fd"));

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomID", "ComplexID", "GenderType", "LeaseEnd", "LeaseStart", "NumberOfBeds", "RoomNumber", "RoomTypeType" },
                values: new object[] { new Guid("4cb45dcc-1426-43c2-b331-368afbfd934b"), new Guid("4949e178-82c5-4fe2-87f2-8143d9d93bc0"), null, new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2019, 11, 15, 11, 28, 17, 335, DateTimeKind.Local).AddTicks(4386), 4, "2428B", null });
        }
    }
}

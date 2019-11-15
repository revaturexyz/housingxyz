using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Revature.Room.DataAccess.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "RoomID",
                keyValue: new Guid("d1962b8d-927a-4575-abf5-a7b8dae959f3"));

            migrationBuilder.InsertData(
                table: "Gender",
                column: "Type",
                values: new object[]
                {
                    "Male",
                    "Female",
                    "NonBinary"
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomID", "ComplexID", "GenderType", "LeaseEnd", "LeaseStart", "NumberOfBeds", "RoomNumber", "RoomTypeType" },
                values: new object[] { new Guid("4cb45dcc-1426-43c2-b331-368afbfd934b"), new Guid("4949e178-82c5-4fe2-87f2-8143d9d93bc0"), null, new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2019, 11, 15, 11, 28, 17, 335, DateTimeKind.Local).AddTicks(4386), 4, "2428B", null });

            migrationBuilder.InsertData(
                table: "RoomType",
                column: "Type",
                values: new object[]
                {
                    "Apartment",
                    "Dormitory",
                    "TownHouse"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Type",
                keyValue: "Female");

            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Type",
                keyValue: "Male");

            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Type",
                keyValue: "NonBinary");

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "RoomID",
                keyValue: new Guid("4cb45dcc-1426-43c2-b331-368afbfd934b"));

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Type",
                keyValue: "Apartment");

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Type",
                keyValue: "Dormitory");

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Type",
                keyValue: "TownHouse");

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomID", "ComplexID", "GenderType", "LeaseEnd", "LeaseStart", "NumberOfBeds", "RoomNumber", "RoomTypeType" },
                values: new object[] { new Guid("d1962b8d-927a-4575-abf5-a7b8dae959f3"), new Guid("ad70c75f-e562-4f8d-891e-a3b7953513f3"), null, new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2019, 11, 15, 11, 17, 26, 974, DateTimeKind.Local).AddTicks(1090), 4, "2428B", null });
        }
    }
}

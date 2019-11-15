using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Revature.Room.DataAccess.Migrations
{
    public partial class updatingdbstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Gender_GenderId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomType_RoomTypeId",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType");

            migrationBuilder.DropIndex(
                name: "IX_Room_GenderId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_RoomTypeId",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gender",
                table: "Gender");

            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: new Guid("e805c884-07c9-475d-9384-a67da9a3d1f6"));

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: new Guid("3dbe5495-af68-4c1d-847f-2f30a859ba93"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomTypeId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Gender");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "RoomType",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenderType",
                table: "Room",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomTypeType",
                table: "Room",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Gender",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType",
                column: "Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gender",
                table: "Gender",
                column: "Type");

            migrationBuilder.InsertData(
                table: "Gender",
                column: "Type",
                value: "NonBinary");

            migrationBuilder.InsertData(
                table: "RoomType",
                column: "Type",
                value: "TownHouse");

            migrationBuilder.CreateIndex(
                name: "IX_Room_GenderType",
                table: "Room",
                column: "GenderType");

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomTypeType",
                table: "Room",
                column: "RoomTypeType");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Gender_GenderType",
                table: "Room",
                column: "GenderType",
                principalTable: "Gender",
                principalColumn: "Type",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomType_RoomTypeType",
                table: "Room",
                column: "RoomTypeType",
                principalTable: "RoomType",
                principalColumn: "Type",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Gender_GenderType",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomType_RoomTypeType",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType");

            migrationBuilder.DropIndex(
                name: "IX_Room_GenderType",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_RoomTypeType",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gender",
                table: "Gender");

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

            migrationBuilder.DropColumn(
                name: "GenderType",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomTypeType",
                table: "Room");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "RoomType",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RoomType",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "Room",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoomTypeId",
                table: "Room",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Gender",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Gender",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gender",
                table: "Gender",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Gender_GenderId",
                table: "Room",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomType_RoomTypeId",
                table: "Room",
                column: "RoomTypeId",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

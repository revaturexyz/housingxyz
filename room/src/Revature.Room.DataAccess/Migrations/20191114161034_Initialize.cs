using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Revature.Room.DataAccess.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomNumber = table.Column<string>(nullable: false),
                    NumberOfBeds = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    RoomType = table.Column<int>(nullable: false),
                    LeaseStart = table.Column<DateTime>(nullable: false),
                    LeaseEnd = table.Column<DateTime>(nullable: false),
                    ComplexID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}

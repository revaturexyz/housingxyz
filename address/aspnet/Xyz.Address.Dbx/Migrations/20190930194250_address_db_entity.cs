using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Xyz.Address.Dbx.Migrations
{
    public partial class address_db_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Key = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    StateProvince = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "varchar", maxLength: 5, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Key",
                table: "Addresses",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}

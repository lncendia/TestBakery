using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakery.Infrastructure.Storage.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BakeTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SellHours = table.Column<int>(type: "integer", nullable: false),
                    InitialCost = table.Column<decimal>(type: "numeric", nullable: false),
                    ControlSellHours = table.Column<int>(type: "integer", nullable: false),
                    BunType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buns", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buns");
        }
    }
}

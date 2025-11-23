using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jellyfin.Server.Implementations.Migrations
{
    /// <inheritdoc />
    public partial class AddItemOptimizationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemOptimizationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OptimizationPreset = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomMaxBitrate = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomVideoCodec = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    CustomAudioCodec = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    CustomMaxWidth = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomMaxHeight = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOptimizationSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemOptimizationSettings_ItemId",
                table: "ItemOptimizationSettings",
                column: "ItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemOptimizationSettings");
        }
    }
}

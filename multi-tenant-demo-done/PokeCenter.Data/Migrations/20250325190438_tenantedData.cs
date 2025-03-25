using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokedex.Data.Migrations
{
    /// <inheritdoc />
    public partial class tenantedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Pods",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Admissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pods_TenantId",
                table: "Pods",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_TenantId",
                table: "Admissions",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pods_TenantId",
                table: "Pods");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_TenantId",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Pods");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Admissions");
        }
    }
}

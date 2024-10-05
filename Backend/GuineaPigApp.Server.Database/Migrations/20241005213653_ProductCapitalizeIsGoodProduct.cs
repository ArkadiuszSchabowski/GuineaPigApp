using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuineaPigApp.Server.Database.Migrations
{
    /// <inheritdoc />
    public partial class ProductCapitalizeIsGoodProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isGoodProduct",
                table: "Products",
                newName: "IsGoodProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsGoodProduct",
                table: "Products",
                newName: "isGoodProduct");
        }
    }
}

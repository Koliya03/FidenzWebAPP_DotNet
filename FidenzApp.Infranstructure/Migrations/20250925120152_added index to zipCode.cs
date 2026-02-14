using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidenzApp.Infranstructure.Migrations
{
    /// <inheritdoc />
    public partial class addedindextozipCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_Tb_address_zipcode",
                table: "Customers_Tb",
                column: "address_zipcode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Tb_address_zipcode",
                table: "Customers_Tb");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FidenzApp.Infranstructure.Migrations
{
    /// <inheritdoc />
    public partial class InitNewDbMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users_Tb",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "AQAAAAIAAYagAAAAEFnpudYT0qGPQ3fT5KtEh/hnqa9VTFNahyEuehNq8QDZefks82Lyma6wigfQfCX3aw==", "Admin", "FidenzAdmin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "AQAAAAIAAYagAAAAEJ3PsujPPv+xtEGluibtrkXbRKVhKnBZm9Tm58MEq39Gy7owNlrwvvjEIRwvb/q0wg==", "User", "FidenzUser" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidenzApp.Infranstructure.Migrations
{
    /// <inheritdoc />
    public partial class InitNewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFnpudYT0qGPQ3fT5KtEh/hnqa9VTFNahyEuehNq8QDZefks82Lyma6wigfQfCX3aw==");

            migrationBuilder.UpdateData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJ3PsujPPv+xtEGluibtrkXbRKVhKnBZm9Tm58MEq39Gy7owNlrwvvjEIRwvb/q0wg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELfOA0PKjNXJslQTfxnaUVHEryX7E7DegI0RaQcQ+IczP814B4r1dxDClndVzAq8wA==");

            migrationBuilder.UpdateData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEO7MjEgqKRIcQlcu8XJD+MCx6LlGZYxf1gjdRqStU02YG+pabui2zXBC/zjbN7Kqg==");
        }
    }
}

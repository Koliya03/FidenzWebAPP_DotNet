using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidenzApp.Infranstructure.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOsPQFgSdu2lZpDi9KcX0Nke7j5ZoaCUHc/Ge0kLpqXuPI37mLyOrEBGZpNe41EuwA==");

            migrationBuilder.UpdateData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOphlhI6+N40s1d0XU8A70Z4aWpUHtTwOw38Qv/mKgpidGLmzv0iYRNyHGp7mYPgNQ==");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidenzApp.Infranstructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEdN9Y0LV515WwrU/pKNDUenHM35c4l6l9JzF7Q620qqlB7KdvG5oDtILQnO2B7LLg==");

            migrationBuilder.UpdateData(
                table: "Users_Tb",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEH9zxTSV3LJh9ptiWUjvNUJR1ZtqdCAWxXP52j9qpMaWyG6MB15ztNqaD0uRRW3JNQ==");
        }
    }
}

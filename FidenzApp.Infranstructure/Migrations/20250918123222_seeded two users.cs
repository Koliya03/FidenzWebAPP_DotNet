using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FidenzApp.Infranstructure.Migrations
{
    /// <inheritdoc />
    public partial class seededtwousers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users_Tb",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "AQAAAAIAAYagAAAAEEdN9Y0LV515WwrU/pKNDUenHM35c4l6l9JzF7Q620qqlB7KdvG5oDtILQnO2B7LLg==", "Admin", "FidenzAdmin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "AQAAAAIAAYagAAAAEH9zxTSV3LJh9ptiWUjvNUJR1ZtqdCAWxXP52j9qpMaWyG6MB15ztNqaD0uRRW3JNQ==", "User", "FidenzUser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}

using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMaaS.Backend.Migrations
{
    /// <inheritdoc />
    public partial class validadteUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailVerificationToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailVerificationTokenExpiry",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenExpiry",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ContentEntries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Data" },
                values: new object[] { new DateTime(2026, 2, 23, 4, 5, 49, 844, DateTimeKind.Utc).AddTicks(3986), System.Text.Json.JsonDocument.Parse("{\"name\":\"Laptop\",\"description\":\"High-performance laptop\",\"price\":999.99,\"category\":\"Electronics\"}", new System.Text.Json.JsonDocumentOptions()) });

            migrationBuilder.UpdateData(
                table: "ContentEntries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Data" },
                values: new object[] { new DateTime(2026, 2, 23, 4, 5, 49, 844, DateTimeKind.Utc).AddTicks(4030), System.Text.Json.JsonDocument.Parse("{\"name\":\"Book\",\"description\":\"Programming guide\",\"price\":29.99,\"category\":\"Education\"}", new System.Text.Json.JsonDocumentOptions()) });

            migrationBuilder.UpdateData(
                table: "ContentEntries",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Data" },
                values: new object[] { new DateTime(2026, 2, 23, 4, 5, 49, 844, DateTimeKind.Utc).AddTicks(4058), System.Text.Json.JsonDocument.Parse("{\"title\":\"Getting Started with CMaaS\",\"content\":\"This is a sample blog post about CMaaS.\",\"author\":\"Admin\",\"publishDate\":\"2023-01-01\"}", new System.Text.Json.JsonDocumentOptions()) });

            migrationBuilder.UpdateData(
                table: "ContentEntries",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Data" },
                values: new object[] { new DateTime(2026, 2, 23, 4, 5, 49, 844, DateTimeKind.Utc).AddTicks(4092), System.Text.Json.JsonDocument.Parse("{\"name\":\"Tablet\",\"price\":299.99,\"stock\":50}", new System.Text.Json.JsonDocumentOptions()) });

            migrationBuilder.UpdateData(
                table: "ContentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Schema",
                value: System.Text.Json.JsonDocument.Parse("{\"type\":\"object\",\"properties\":{\"name\":{\"type\":\"string\"},\"description\":{\"type\":\"string\"},\"price\":{\"type\":\"number\"},\"category\":{\"type\":\"string\"}},\"required\":[\"name\",\"price\"]}", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ContentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Schema",
                value: System.Text.Json.JsonDocument.Parse("{\"type\":\"object\",\"properties\":{\"title\":{\"type\":\"string\"},\"content\":{\"type\":\"string\"},\"author\":{\"type\":\"string\"},\"publishDate\":{\"type\":\"string\",\"format\":\"date\"}},\"required\":[\"title\",\"content\"]}", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ContentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Schema",
                value: System.Text.Json.JsonDocument.Parse("{\"type\":\"object\",\"properties\":{\"name\":{\"type\":\"string\"},\"price\":{\"type\":\"number\"},\"stock\":{\"type\":\"integer\"}},\"required\":[\"name\"]}", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 4, 5, 49, 844, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 4, 5, 49, 844, DateTimeKind.Utc).AddTicks(3381));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EmailVerificationToken", "EmailVerificationTokenExpiry", "IsEmailVerified", "PasswordResetToken", "PasswordResetTokenExpiry" },
                values: new object[] { null, null, false, null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EmailVerificationToken", "EmailVerificationTokenExpiry", "IsEmailVerified", "PasswordResetToken", "PasswordResetTokenExpiry" },
                values: new object[] { null, null, false, null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EmailVerificationToken", "EmailVerificationTokenExpiry", "IsEmailVerified", "PasswordResetToken", "PasswordResetTokenExpiry" },
                values: new object[] { null, null, false, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerificationToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailVerificationTokenExpiry",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpiry",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "ContentEntries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Data" },
                values: new object[] { new DateTime(2026, 2, 14, 13, 15, 24, 912, DateTimeKind.Utc).AddTicks(4184), System.Text.Json.JsonDocument.Parse("{\"name\":\"Laptop\",\"description\":\"High-performance laptop\",\"price\":999.99,\"category\":\"Electronics\"}", new System.Text.Json.JsonDocumentOptions()) });

            migrationBuilder.UpdateData(
                table: "ContentEntries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Data" },
                values: new object[] { new DateTime(2026, 2, 14, 13, 15, 24, 912, DateTimeKind.Utc).AddTicks(4224), System.Text.Json.JsonDocument.Parse("{\"name\":\"Book\",\"description\":\"Programming guide\",\"price\":29.99,\"category\":\"Education\"}", new System.Text.Json.JsonDocumentOptions()) });

            migrationBuilder.UpdateData(
                table: "ContentEntries",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Data" },
                values: new object[] { new DateTime(2026, 2, 14, 13, 15, 24, 912, DateTimeKind.Utc).AddTicks(4251), System.Text.Json.JsonDocument.Parse("{\"title\":\"Getting Started with CMaaS\",\"content\":\"This is a sample blog post about CMaaS.\",\"author\":\"Admin\",\"publishDate\":\"2023-01-01\"}", new System.Text.Json.JsonDocumentOptions()) });

            migrationBuilder.UpdateData(
                table: "ContentEntries",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Data" },
                values: new object[] { new DateTime(2026, 2, 14, 13, 15, 24, 912, DateTimeKind.Utc).AddTicks(4283), System.Text.Json.JsonDocument.Parse("{\"name\":\"Tablet\",\"price\":299.99,\"stock\":50}", new System.Text.Json.JsonDocumentOptions()) });

            migrationBuilder.UpdateData(
                table: "ContentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Schema",
                value: System.Text.Json.JsonDocument.Parse("{\"type\":\"object\",\"properties\":{\"name\":{\"type\":\"string\"},\"description\":{\"type\":\"string\"},\"price\":{\"type\":\"number\"},\"category\":{\"type\":\"string\"}},\"required\":[\"name\",\"price\"]}", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ContentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Schema",
                value: System.Text.Json.JsonDocument.Parse("{\"type\":\"object\",\"properties\":{\"title\":{\"type\":\"string\"},\"content\":{\"type\":\"string\"},\"author\":{\"type\":\"string\"},\"publishDate\":{\"type\":\"string\",\"format\":\"date\"}},\"required\":[\"title\",\"content\"]}", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ContentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Schema",
                value: System.Text.Json.JsonDocument.Parse("{\"type\":\"object\",\"properties\":{\"name\":{\"type\":\"string\"},\"price\":{\"type\":\"number\"},\"stock\":{\"type\":\"integer\"}},\"required\":[\"name\"]}", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 13, 15, 24, 912, DateTimeKind.Utc).AddTicks(3657));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 13, 15, 24, 912, DateTimeKind.Utc).AddTicks(3659));
        }
    }
}

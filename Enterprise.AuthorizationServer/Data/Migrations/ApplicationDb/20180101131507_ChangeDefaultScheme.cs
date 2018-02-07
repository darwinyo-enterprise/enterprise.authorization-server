using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Enterprise.AuthorizationServer.Data.Migrations.ApplicationDb
{
    public partial class ChangeDefaultScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Authentication");

            migrationBuilder.RenameTable(
                name: "UserToken",
                schema: "Authorization",
                newSchema: "Authentication");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Authorization",
                newSchema: "Authentication");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Authorization",
                newSchema: "Authentication");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "Authorization",
                newSchema: "Authentication");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "Authorization",
                newSchema: "Authentication");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Authorization",
                newSchema: "Authentication");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                schema: "Authorization",
                newSchema: "Authentication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Authorization");

            migrationBuilder.RenameTable(
                name: "UserToken",
                schema: "Authentication",
                newSchema: "Authorization");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Authentication",
                newSchema: "Authorization");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Authentication",
                newSchema: "Authorization");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "Authentication",
                newSchema: "Authorization");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "Authentication",
                newSchema: "Authorization");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Authentication",
                newSchema: "Authorization");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                schema: "Authentication",
                newSchema: "Authorization");
        }
    }
}

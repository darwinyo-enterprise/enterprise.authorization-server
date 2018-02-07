using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Enterprise.AuthorizationServer.Data.Migrations.ApplicationDb
{
    public partial class RebindRoleToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntegratedAppID",
                schema: "Authentication",
                table: "ApplicationRoles",
                newName: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "Authentication",
                table: "ApplicationRoles",
                newName: "IntegratedAppID");
        }
    }
}

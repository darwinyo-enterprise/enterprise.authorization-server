using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Enterprise.AuthorizationServer.Data.Migrations.ApplicationDb
{
    public partial class RenameEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_Roles_RoleId",
                schema: "Authentication",
                table: "RoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_UserId",
                schema: "Authentication",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "Authentication",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "Authentication",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "Authentication",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_Users_UserId",
                schema: "Authentication",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToken",
                schema: "Authentication",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "Authentication",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "Authentication",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                schema: "Authentication",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                schema: "Authentication",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "Authentication",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaim",
                schema: "Authentication",
                table: "RoleClaim");

            migrationBuilder.RenameTable(
                name: "UserToken",
                schema: "Authentication",
                newName: "ApplicationUserToken");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Authentication",
                newName: "ApplicationUser");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Authentication",
                newName: "ApplicationUserRoles");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "Authentication",
                newName: "ApplicationUserLogins");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "Authentication",
                newName: "ApplicationUserClaims");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Authentication",
                newName: "ApplicationRoles");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                schema: "Authentication",
                newName: "ApplicationRoleClaim");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Authentication",
                table: "ApplicationUserRoles",
                newName: "IX_ApplicationUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                schema: "Authentication",
                table: "ApplicationUserLogins",
                newName: "IX_ApplicationUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                schema: "Authentication",
                table: "ApplicationUserClaims",
                newName: "IX_ApplicationUserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "AppID",
                schema: "Authentication",
                table: "ApplicationRoles",
                newName: "IntegratedAppID");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "Authentication",
                table: "ApplicationRoleClaim",
                newName: "IX_ApplicationRoleClaim_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserToken",
                schema: "Authentication",
                table: "ApplicationUserToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                schema: "Authentication",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRoles",
                schema: "Authentication",
                table: "ApplicationUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserLogins",
                schema: "Authentication",
                table: "ApplicationUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserClaims",
                schema: "Authentication",
                table: "ApplicationUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationRoles",
                schema: "Authentication",
                table: "ApplicationRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationRoleClaim",
                schema: "Authentication",
                table: "ApplicationRoleClaim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRoleClaim_ApplicationRoles_RoleId",
                schema: "Authentication",
                table: "ApplicationRoleClaim",
                column: "RoleId",
                principalSchema: "Authentication",
                principalTable: "ApplicationRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserClaims_ApplicationUser_UserId",
                schema: "Authentication",
                table: "ApplicationUserClaims",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserLogins_ApplicationUser_UserId",
                schema: "Authentication",
                table: "ApplicationUserLogins",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRoles_ApplicationRoles_RoleId",
                schema: "Authentication",
                table: "ApplicationUserRoles",
                column: "RoleId",
                principalSchema: "Authentication",
                principalTable: "ApplicationRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRoles_ApplicationUser_UserId",
                schema: "Authentication",
                table: "ApplicationUserRoles",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserToken_ApplicationUser_UserId",
                schema: "Authentication",
                table: "ApplicationUserToken",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRoleClaim_ApplicationRoles_RoleId",
                schema: "Authentication",
                table: "ApplicationRoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserClaims_ApplicationUser_UserId",
                schema: "Authentication",
                table: "ApplicationUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserLogins_ApplicationUser_UserId",
                schema: "Authentication",
                table: "ApplicationUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRoles_ApplicationRoles_RoleId",
                schema: "Authentication",
                table: "ApplicationUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRoles_ApplicationUser_UserId",
                schema: "Authentication",
                table: "ApplicationUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserToken_ApplicationUser_UserId",
                schema: "Authentication",
                table: "ApplicationUserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserToken",
                schema: "Authentication",
                table: "ApplicationUserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRoles",
                schema: "Authentication",
                table: "ApplicationUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserLogins",
                schema: "Authentication",
                table: "ApplicationUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserClaims",
                schema: "Authentication",
                table: "ApplicationUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                schema: "Authentication",
                table: "ApplicationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationRoles",
                schema: "Authentication",
                table: "ApplicationRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationRoleClaim",
                schema: "Authentication",
                table: "ApplicationRoleClaim");

            migrationBuilder.RenameTable(
                name: "ApplicationUserToken",
                schema: "Authentication",
                newName: "UserToken");

            migrationBuilder.RenameTable(
                name: "ApplicationUserRoles",
                schema: "Authentication",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "ApplicationUserLogins",
                schema: "Authentication",
                newName: "UserLogins");

            migrationBuilder.RenameTable(
                name: "ApplicationUserClaims",
                schema: "Authentication",
                newName: "UserClaims");

            migrationBuilder.RenameTable(
                name: "ApplicationUser",
                schema: "Authentication",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ApplicationRoles",
                schema: "Authentication",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "ApplicationRoleClaim",
                schema: "Authentication",
                newName: "RoleClaim");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserRoles_RoleId",
                schema: "Authentication",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserLogins_UserId",
                schema: "Authentication",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserClaims_UserId",
                schema: "Authentication",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "IntegratedAppID",
                schema: "Authentication",
                table: "Roles",
                newName: "AppID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationRoleClaim_RoleId",
                schema: "Authentication",
                table: "RoleClaim",
                newName: "IX_RoleClaim_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToken",
                schema: "Authentication",
                table: "UserToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "Authentication",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                schema: "Authentication",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                schema: "Authentication",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "Authentication",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "Authentication",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaim",
                schema: "Authentication",
                table: "RoleClaim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_Roles_RoleId",
                schema: "Authentication",
                table: "RoleClaim",
                column: "RoleId",
                principalSchema: "Authentication",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                schema: "Authentication",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "Authentication",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "Authentication",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "Authentication",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "Authentication",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_Users_UserId",
                schema: "Authentication",
                table: "UserToken",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

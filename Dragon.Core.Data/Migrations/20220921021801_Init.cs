using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dragon.Core.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApiName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ApiUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysDepartMent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Pid = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDepartMent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    MenuType = table.Column<int>(type: "INTEGER", nullable: false),
                    path = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Component = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Permission = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Redirect = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    FrameSrc = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IgnoreKeepAlive = table.Column<bool>(type: "INTEGER", nullable: true),
                    HideMenu = table.Column<bool>(type: "INTEGER", nullable: true),
                    CurrentActiveMenu = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ParId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mid = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DataScope = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleMenuModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mid = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleMenuModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Sex = table.Column<bool>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    WeChat = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Account = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    HistroyPsw = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ErrorCount = table.Column<int>(type: "INTEGER", nullable: false),
                    LastErrTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpliyeeType = table.Column<int>(type: "INTEGER", nullable: false),
                    JobName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Avater = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUserDepartMent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartMentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserDepartMent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartMent_Name",
                table: "SysDepartMent",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_Name",
                table: "SysRole",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SysUser_Name",
                table: "SysUser",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiModule");

            migrationBuilder.DropTable(
                name: "SysDepartMent");

            migrationBuilder.DropTable(
                name: "SysMenu");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "SysRoleMenuModule");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "SysUserDepartMent");

            migrationBuilder.DropTable(
                name: "SysUserRole");
        }
    }
}

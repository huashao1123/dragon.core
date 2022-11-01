using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dragon.Core.Data.Migrations
{
    public partial class updateusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysUserDepartMent");

            migrationBuilder.AddColumn<int>(
                name: "JobStatus",
                table: "SysUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PosId",
                table: "SysUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "SysUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobStatus",
                table: "SysUser");

            migrationBuilder.DropColumn(
                name: "PosId",
                table: "SysUser");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "SysUser");

            migrationBuilder.CreateTable(
                name: "SysUserDepartMent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DepartMentId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserDepartMent", x => x.Id);
                });
        }
    }
}

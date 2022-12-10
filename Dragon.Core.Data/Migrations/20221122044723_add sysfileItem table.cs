using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dragon.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class addsysfileItemtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysFileItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileSizeInBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FileSHA256Hash = table.Column<string>(type: "TEXT", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    Suffix = table.Column<string>(type: "TEXT", nullable: false),
                    FileVersion = table.Column<string>(type: "TEXT", nullable: false),
                    FileOwnDept = table.Column<string>(type: "TEXT", nullable: true),
                    DeptId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileType = table.Column<string>(type: "TEXT", nullable: true),
                    FileLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDrop = table.Column<bool>(type: "INTEGER", nullable: true),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysFileItem", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysFileItem");
        }
    }
}

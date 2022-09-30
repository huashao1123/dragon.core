using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dragon.Core.Data.Migrations
{
    public partial class updatesomecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "SysUserRole",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "SysUserDepartMent",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "SysUser",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "SysRoleMenuModule",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "SysRole",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "SysMenu",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "SysDepartMent",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "ApiModule",
                newName: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "SysUserRole",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "SysUserDepartMent",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "SysUser",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "SysRoleMenuModule",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "SysRole",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "SysMenu",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "SysDepartMent",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "ApiModule",
                newName: "OrderNo");
        }
    }
}

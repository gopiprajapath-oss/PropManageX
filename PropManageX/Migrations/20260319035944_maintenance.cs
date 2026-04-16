using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropManageX.Migrations
{
    /// <inheritdoc />
    public partial class maintenance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Units_UnitID",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Users_TenantID",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorAssignments_MaintenanceRequests_RequestID",
                table: "VendorAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "RequestID",
                table: "VendorAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitID",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenantID",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Units_UnitID",
                table: "MaintenanceRequests",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "UnitID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Users_TenantID",
                table: "MaintenanceRequests",
                column: "TenantID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorAssignments_MaintenanceRequests_RequestID",
                table: "VendorAssignments",
                column: "RequestID",
                principalTable: "MaintenanceRequests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Units_UnitID",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Users_TenantID",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorAssignments_MaintenanceRequests_RequestID",
                table: "VendorAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "RequestID",
                table: "VendorAssignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UnitID",
                table: "MaintenanceRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TenantID",
                table: "MaintenanceRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Units_UnitID",
                table: "MaintenanceRequests",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Users_TenantID",
                table: "MaintenanceRequests",
                column: "TenantID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorAssignments_MaintenanceRequests_RequestID",
                table: "VendorAssignments",
                column: "RequestID",
                principalTable: "MaintenanceRequests",
                principalColumn: "RequestID");
        }
    }
}

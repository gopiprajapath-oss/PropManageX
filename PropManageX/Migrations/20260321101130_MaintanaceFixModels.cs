using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropManageX.Migrations
{
    /// <inheritdoc />
    public partial class MaintanaceFixModels : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Units_UnitID",
                table: "MaintenanceRequests",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "UnitID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Users_TenantID",
                table: "MaintenanceRequests",
                column: "TenantID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorAssignments_MaintenanceRequests_RequestID",
                table: "VendorAssignments",
                column: "RequestID",
                principalTable: "MaintenanceRequests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Restrict);
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
    }
}

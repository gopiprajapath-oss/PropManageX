using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropManageX.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadingPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Leads_LeadID",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Units_UnitID",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Properties_PropertyID",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "UnitID",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LeadID",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Leads_LeadID",
                table: "Deals",
                column: "LeadID",
                principalTable: "Leads",
                principalColumn: "LeadID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Units_UnitID",
                table: "Deals",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "UnitID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits",
                column: "LeadID",
                principalTable: "Leads",
                principalColumn: "LeadID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Properties_PropertyID",
                table: "Units",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Leads_LeadID",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Units_UnitID",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Properties_PropertyID",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "UnitID",
                table: "Deals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LeadID",
                table: "Deals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Leads_LeadID",
                table: "Deals",
                column: "LeadID",
                principalTable: "Leads",
                principalColumn: "LeadID");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Units_UnitID",
                table: "Deals",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits",
                column: "LeadID",
                principalTable: "Leads",
                principalColumn: "LeadID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Properties_PropertyID",
                table: "Units",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

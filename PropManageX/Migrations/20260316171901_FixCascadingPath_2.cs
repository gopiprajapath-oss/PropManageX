using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropManageX.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadingPath_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Deals_DealID",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Contracts_ContractID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Renewals_Contracts_ContractID",
                table: "Renewals");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Properties_PropertyID",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "ContractID",
                table: "Renewals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContractID",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DealID",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Deals_DealID",
                table: "Contracts",
                column: "DealID",
                principalTable: "Deals",
                principalColumn: "DealID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Contracts_ContractID",
                table: "Invoices",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renewals_Contracts_ContractID",
                table: "Renewals",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Deals_DealID",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Contracts_ContractID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Renewals_Contracts_ContractID",
                table: "Renewals");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Properties_PropertyID",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "ContractID",
                table: "Renewals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContractID",
                table: "Invoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DealID",
                table: "Contracts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Deals_DealID",
                table: "Contracts",
                column: "DealID",
                principalTable: "Deals",
                principalColumn: "DealID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Contracts_ContractID",
                table: "Invoices",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_Renewals_Contracts_ContractID",
                table: "Renewals",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "ContractID");

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
    }
}

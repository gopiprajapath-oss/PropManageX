using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropManageX.Migrations
{
    /// <inheritdoc />
    public partial class propertyDeletiontwo : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Deals_DealID",
                table: "Contracts",
                column: "DealID",
                principalTable: "Deals",
                principalColumn: "DealID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Contracts_ContractID",
                table: "Invoices",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Renewals_Contracts_ContractID",
                table: "Renewals",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "ContractID",
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
                onDelete: ReferentialAction.Restrict);
        }
    }
}

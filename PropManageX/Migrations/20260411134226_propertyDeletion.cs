using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropManageX.Migrations
{
    /// <inheritdoc />
    public partial class propertyDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Properties_PropertyID",
                table: "Amenities");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Properties_PropertyID",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Renewals_Contracts_ContractID",
                table: "Renewals");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVisits_Users_AgentID",
                table: "SiteVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Properties_PropertyID",
                table: "Units");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Properties_PropertyID",
                table: "Amenities",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Properties_PropertyID",
                table: "Leads",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renewals_Contracts_ContractID",
                table: "Renewals",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits",
                column: "LeadID",
                principalTable: "Leads",
                principalColumn: "LeadID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteVisits_Users_AgentID",
                table: "SiteVisits",
                column: "AgentID",
                principalTable: "Users",
                principalColumn: "UserID",
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
                name: "FK_Amenities_Properties_PropertyID",
                table: "Amenities");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Properties_PropertyID",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Renewals_Contracts_ContractID",
                table: "Renewals");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVisits_Leads_LeadID",
                table: "SiteVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVisits_Users_AgentID",
                table: "SiteVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Properties_PropertyID",
                table: "Units");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Properties_PropertyID",
                table: "Amenities",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Properties_PropertyID",
                table: "Leads",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_SiteVisits_Users_AgentID",
                table: "SiteVisits",
                column: "AgentID",
                principalTable: "Users",
                principalColumn: "UserID",
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

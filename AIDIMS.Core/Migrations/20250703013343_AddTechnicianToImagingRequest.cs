using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDIMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddTechnicianToImagingRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TechnicianID",
                table: "ImagingRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagingRequests_TechnicianID",
                table: "ImagingRequests",
                column: "TechnicianID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagingRequests_HospitalStaffs_TechnicianID",
                table: "ImagingRequests",
                column: "TechnicianID",
                principalTable: "HospitalStaffs",
                principalColumn: "StaffID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagingRequests_HospitalStaffs_TechnicianID",
                table: "ImagingRequests");

            migrationBuilder.DropIndex(
                name: "IX_ImagingRequests_TechnicianID",
                table: "ImagingRequests");

            migrationBuilder.DropColumn(
                name: "TechnicianID",
                table: "ImagingRequests");
        }
    }
}

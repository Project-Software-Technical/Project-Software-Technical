using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDIMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class FillNullEmails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"Users\" SET \"Email\" = CONCAT('staff', \"StaffID\", '@placeholder.local') WHERE \"Email\" IS NULL;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

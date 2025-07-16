using Microsoft.EntityFrameworkCore.Migrations;

namespace ClaimOrchestrator.Migrations
{
    public partial class RemoveClaimsAndProcessingLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessingLogs");
            migrationBuilder.DropTable(
                name: "Claims");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optionally, recreate tables if needed for rollback
        }
    }
}

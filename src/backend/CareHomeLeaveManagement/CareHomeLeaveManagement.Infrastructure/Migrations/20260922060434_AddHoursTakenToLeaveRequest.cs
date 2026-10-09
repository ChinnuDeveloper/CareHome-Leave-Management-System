using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareHomeLeaveManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHoursTakenToLeaveRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HoursTaken",
                table: "LeaveRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoursTaken",
                table: "LeaveRequests");
        }
    }
}

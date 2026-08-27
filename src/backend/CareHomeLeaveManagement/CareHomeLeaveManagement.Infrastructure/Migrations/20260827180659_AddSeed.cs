using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CareHomeLeaveManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName", "MaxConcurrentLeave" },
                values: new object[,]
                {
                    { 1, "Carer", 2 },
                    { 2, "Domestic Worker", 1 },
                    { 3, "Nurse", 1 },
                    { 4, "Kitchen Staff", 1 }
                });

            migrationBuilder.InsertData(
                table: "FiscalYears",
                columns: new[] { "FiscalYearId", "GeneratedBy", "GeneratedOn", "Year" },
                values: new object[,]
                {
                    { 1, null, null, "2026-2027" },
                    { 2, null, null, "2027-2028" },
                    { 3, null, null, "2028-2029" },
                    { 4, null, null, "2029-2030" },
                    { 5, null, null, "2030-2031" }
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "LeaveTypeId", "Name", "RequiresEntitlement" },
                values: new object[] { 1, "Annual Leave", true });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "LeaveTypeId", "Name" },
                values: new object[] { 2, "Birthday Leave" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FiscalYears",
                keyColumn: "FiscalYearId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FiscalYears",
                keyColumn: "FiscalYearId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FiscalYears",
                keyColumn: "FiscalYearId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FiscalYears",
                keyColumn: "FiscalYearId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FiscalYears",
                keyColumn: "FiscalYearId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "LeaveTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "LeaveTypeId",
                keyValue: 2);
        }
    }
}

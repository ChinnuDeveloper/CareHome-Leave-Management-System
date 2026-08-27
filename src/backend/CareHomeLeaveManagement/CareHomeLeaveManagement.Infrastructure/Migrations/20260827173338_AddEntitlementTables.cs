using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareHomeLeaveManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitlementTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiscalYears",
                columns: table => new
                {
                    FiscalYearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GeneratedBy = table.Column<int>(type: "int", nullable: true),
                    GeneratedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYears", x => x.FiscalYearId);
                    table.ForeignKey(
                        name: "FK_FiscalYears_Employees_GeneratedBy",
                        column: x => x.GeneratedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entitlements",
                columns: table => new
                {
                    EntitlementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    FiscalYearId = table.Column<int>(type: "int", nullable: false),
                    AllocatedHrs = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TakenHrs = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitlements", x => x.EntitlementId);
                    table.ForeignKey(
                        name: "FK_Entitlements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entitlements_FiscalYears_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYears",
                        principalColumn: "FiscalYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entitlements_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "LeaveTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntitlementHistories",
                columns: table => new
                {
                    EntitlementHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntitlementId = table.Column<int>(type: "int", nullable: false),
                    OldAllocationHrs = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OldTakenHrs = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NewAllocationHrs = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NewTakenHrs = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitlementHistories", x => x.EntitlementHistoryId);
                    table.ForeignKey(
                        name: "FK_EntitlementHistories_Employees_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntitlementHistories_Entitlements_EntitlementId",
                        column: x => x.EntitlementId,
                        principalTable: "Entitlements",
                        principalColumn: "EntitlementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntitlementHistories_EntitlementId",
                table: "EntitlementHistories",
                column: "EntitlementId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitlementHistories_ModifiedBy",
                table: "EntitlementHistories",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entitlements_EmployeeId",
                table: "Entitlements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitlements_FiscalYearId",
                table: "Entitlements",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitlements_LeaveTypeId",
                table: "Entitlements",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_GeneratedBy",
                table: "FiscalYears",
                column: "GeneratedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntitlementHistories");

            migrationBuilder.DropTable(
                name: "Entitlements");

            migrationBuilder.DropTable(
                name: "FiscalYears");
        }
    }
}

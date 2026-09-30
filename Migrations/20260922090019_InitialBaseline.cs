using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialBaseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    stateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stateName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_state", x => x.stateId);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    districtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    districtName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    stateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_distId", x => x.districtId);
                    table.ForeignKey(
                        name: "fk_stateId",
                        column: x => x.stateId,
                        principalTable: "states",
                        principalColumn: "stateId");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    empId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    empGender = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    empDOB = table.Column<DateOnly>(type: "date", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    stateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_empId", x => x.empId);
                    table.ForeignKey(
                        name: "FK_Employee_districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "districts",
                        principalColumn: "districtId");
                    table.ForeignKey(
                        name: "fk_stateId1",
                        column: x => x.stateId,
                        principalTable: "states",
                        principalColumn: "stateId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_districts_stateId",
                table: "districts",
                column: "stateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DistrictId",
                table: "Employee",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_stateId",
                table: "Employee",
                column: "stateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "states");
        }
    }
}

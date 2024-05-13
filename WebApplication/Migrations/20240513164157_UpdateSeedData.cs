using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEmployees",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmployees", x => new { x.ProjectId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaryAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6af2bec5-9dbf-431e-b542-59f7dc54ad68"), "HR" },
                    { new Guid("82421c74-579d-40ab-b104-c3435c42d1ab"), "Finance" },
                    { new Guid("aca5dbe5-f96d-46a2-b8b5-6a676dddd01b"), "Software Development" },
                    { new Guid("de277e98-f157-47b3-9636-14c30bf2fc4f"), "Accountant" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00d231d1-113c-4253-884b-ff035e2c5f79"), "Project B" },
                    { new Guid("298d8fde-6901-4505-b341-e7345e17e2f3"), "Project A" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "JoinedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("1bb38781-9a96-4f8f-ace6-0f66247ffda9"), new Guid("aca5dbe5-f96d-46a2-b8b5-6a676dddd01b"), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đặng Phan Thành Công" },
                    { new Guid("65f5df7b-aa7d-4d67-a479-1273642eefb2"), new Guid("6af2bec5-9dbf-431e-b542-59f7dc54ad68"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phan Thị Thu Hà" },
                    { new Guid("6991947f-7f9d-4d31-8fd5-f2a5517a314e"), new Guid("82421c74-579d-40ab-b104-c3435c42d1ab"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Mỹ Linh" },
                    { new Guid("ad537c8c-e592-4292-9eef-a517dfaf8dac"), new Guid("de277e98-f157-47b3-9636-14c30bf2fc4f"), new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trần Thị Minh Phương" }
                });

            migrationBuilder.InsertData(
                table: "ProjectEmployees",
                columns: new[] { "EmployeeId", "ProjectId" },
                values: new object[,]
                {
                    { new Guid("65f5df7b-aa7d-4d67-a479-1273642eefb2"), new Guid("00d231d1-113c-4253-884b-ff035e2c5f79") },
                    { new Guid("6991947f-7f9d-4d31-8fd5-f2a5517a314e"), new Guid("00d231d1-113c-4253-884b-ff035e2c5f79") },
                    { new Guid("ad537c8c-e592-4292-9eef-a517dfaf8dac"), new Guid("00d231d1-113c-4253-884b-ff035e2c5f79") },
                    { new Guid("1bb38781-9a96-4f8f-ace6-0f66247ffda9"), new Guid("298d8fde-6901-4505-b341-e7345e17e2f3") },
                    { new Guid("65f5df7b-aa7d-4d67-a479-1273642eefb2"), new Guid("298d8fde-6901-4505-b341-e7345e17e2f3") },
                    { new Guid("6991947f-7f9d-4d31-8fd5-f2a5517a314e"), new Guid("298d8fde-6901-4505-b341-e7345e17e2f3") },
                    { new Guid("ad537c8c-e592-4292-9eef-a517dfaf8dac"), new Guid("298d8fde-6901-4505-b341-e7345e17e2f3") }
                });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "Id", "EmployeeId", "SalaryAmount" },
                values: new object[,]
                {
                    { new Guid("65c73311-493b-45ac-9830-6a3108e701d1"), new Guid("ad537c8c-e592-4292-9eef-a517dfaf8dac"), 150m },
                    { new Guid("6c1c65ac-f098-469d-91d3-c0741270d363"), new Guid("6991947f-7f9d-4d31-8fd5-f2a5517a314e"), 120m },
                    { new Guid("80c1736a-ff7b-4177-8e0a-fbcb72983e0a"), new Guid("1bb38781-9a96-4f8f-ace6-0f66247ffda9"), 100m },
                    { new Guid("fbafe064-aa1f-4529-be2b-2e8784080aa6"), new Guid("65f5df7b-aa7d-4d67-a479-1273642eefb2"), 200m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployees_EmployeeId",
                table: "ProjectEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectEmployees");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fablab.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectName);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierName);
                });

            migrationBuilder.CreateTable(
                name: "Borrow",
                columns: table => new
                {
                    BorrowID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BorrowedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Borrower = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnSide = table.Column<bool>(type: "bit", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrow", x => x.BorrowID);
                    table.ForeignKey(
                        name: "FK_Borrow_Project_ProjectName",
                        column: x => x.ProjectName,
                        principalTable: "Project",
                        principalColumn: "ProjectName");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfSupply = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeOfManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EquipmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK_Equipment_Suppliers_SupplierName",
                        column: x => x.SupplierName,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierName");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentBorrows",
                columns: table => new
                {
                    BorrowID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentBorrows", x => new { x.BorrowID, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_EquipmentBorrows_Borrow_BorrowID",
                        column: x => x.BorrowID,
                        principalTable: "Borrow",
                        principalColumn: "BorrowID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentBorrows_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_ProjectName",
                table: "Borrow",
                column: "ProjectName");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LocationID",
                table: "Equipment",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_SupplierName",
                table: "Equipment",
                column: "SupplierName");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentBorrows_EquipmentId",
                table: "EquipmentBorrows",
                column: "EquipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentBorrows");

            migrationBuilder.DropTable(
                name: "Borrow");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}

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
                    EquipmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipmentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.EquipmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierName);
                });

            migrationBuilder.CreateTable(
                name: "Borrow",
                columns: table => new
                {
                    BorrowId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BorrowedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RealReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Borrower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnSide = table.Column<bool>(type: "bit", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrow", x => x.BorrowId);
                    table.ForeignKey(
                        name: "FK_Borrow_Project_ProjectName",
                        column: x => x.ProjectName,
                        principalTable: "Project",
                        principalColumn: "ProjectName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfSupply = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeOfManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        principalColumn: "EquipmentTypeId");
                    table.ForeignKey(
                        name: "FK_Equipment_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK_Equipment_Suppliers_SupplierName",
                        column: x => x.SupplierName,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowEquipment",
                columns: table => new
                {
                    BorrowsBorrowId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EquipmentsEquipmentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowEquipment", x => new { x.BorrowsBorrowId, x.EquipmentsEquipmentId });
                    table.ForeignKey(
                        name: "FK_BorrowEquipment_Borrow_BorrowsBorrowId",
                        column: x => x.BorrowsBorrowId,
                        principalTable: "Borrow",
                        principalColumn: "BorrowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowEquipment_Equipment_EquipmentsEquipmentId",
                        column: x => x.EquipmentsEquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_ProjectName",
                table: "Borrow",
                column: "ProjectName");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowEquipment_EquipmentsEquipmentId",
                table: "BorrowEquipment",
                column: "EquipmentsEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LocationId",
                table: "Equipment",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_SupplierName",
                table: "Equipment",
                column: "SupplierName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowEquipment");

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

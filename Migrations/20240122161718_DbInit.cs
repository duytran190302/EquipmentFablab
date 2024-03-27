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
                name: "EquipmentType",
                columns: table => new
                {
                    EquipmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EquipmentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentType", x => x.EquipmentTypeId);
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
                    RealEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectName);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierName);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    PictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileData = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => new { x.EquipmentTypeId, x.PictureId });
                    table.ForeignKey(
                        name: "FK_Picture_EquipmentType_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentType",
                        principalColumn: "EquipmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specification",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EquipmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specification", x => new { x.EquipmentTypeId, x.Name });
                    table.ForeignKey(
                        name: "FK_Specification_EquipmentType_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentType",
                        principalColumn: "EquipmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tag_EquipmentType_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentType",
                        principalColumn: "EquipmentTypeId");
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
                    EquipmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipmentType_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentType",
                        principalColumn: "EquipmentTypeId");
                    table.ForeignKey(
                        name: "FK_Equipment_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK_Equipment_Project_ProjectName",
                        column: x => x.ProjectName,
                        principalTable: "Project",
                        principalColumn: "ProjectName");
                    table.ForeignKey(
                        name: "FK_Equipment_Supplier_SupplierName",
                        column: x => x.SupplierName,
                        principalTable: "Supplier",
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
                name: "IX_Equipment_ProjectName",
                table: "Equipment",
                column: "ProjectName");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_SupplierName",
                table: "Equipment",
                column: "SupplierName");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EquipmentTypeId",
                table: "Tag",
                column: "EquipmentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowEquipment");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "Specification");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Borrow");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipmentType");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}

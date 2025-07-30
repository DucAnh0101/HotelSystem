using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PriceForRent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CitizenId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    AvtUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsMale = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Picture_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Manager_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Staffs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkShiftStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkShiftEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attendance_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Booking_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkShift",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    WorkShipStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkShipEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShift", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkShift_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkShift_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkShift_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "ID", "Description", "PriceForRent", "PricePerHour", "Status", "WorkId" },
                values: new object[,]
                {
                    { 1, "Phòng đơn tiêu chuẩn với đầy đủ tiện nghi", 500000m, 50000m, 2, null },
                    { 2, "Phòng đôi cao cấp với view đẹp", 800000m, 80000m, 2, null },
                    { 3, "Phòng suite sang trọng với phòng khách riêng", 1200000m, 120000m, 1, null },
                    { 4, "Phòng gia đình rộng rãi", 600000m, 60000m, 2, null },
                    { 5, "Phòng VIP với jacuzzi", 700000m, 70000m, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "AccountId", "AvtUrl", "CitizenId", "DOB", "FullName", "IsMale", "Role" },
                values: new object[,]
                {
                    { 1, null, "https://example.com/avatar1.jpg", "123456789012", new DateOnly(1985, 5, 15), "A", true, 0 },
                    { 2, null, "https://example.com/avatar2.jpg", "234567890123", new DateOnly(1988, 8, 20), "B", false, 2 },
                    { 3, null, "https://example.com/avatar3.jpg", "345678901234", new DateOnly(1992, 3, 10), "C", true, 3 },
                    { 4, null, "https://example.com/avatar4.jpg", "456789012345", new DateOnly(1990, 12, 5), "D", false, 3 },
                    { 5, null, "https://example.com/avatar5.jpg", "567890123456", new DateOnly(1995, 7, 25), "E", true, 1 },
                    { 6, null, "https://example.com/avatar6.jpg", "678901234567", new DateOnly(1993, 11, 18), "F", false, 1 },
                    { 7, null, "https://example.com/avatar7.jpg", "789012345678", new DateOnly(1987, 4, 30), "G", true, 1 }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "DOB", "Email", "IsDelete", "Password", "PhoneNumber", "UserId", "UserName" },
                values: new object[,]
                {
                    { 1, new DateOnly(1, 1, 1), "admin@hotel.com", false, "admin123", "0123456789", 1, "admin" },
                    { 2, new DateOnly(1, 1, 1), "manager1@hotel.com", false, "manager123", "0987654321", 2, "manager1" },
                    { 3, new DateOnly(1, 1, 1), "staff1@hotel.com", false, "staff123", "0912345678", 3, "staff1" },
                    { 4, new DateOnly(1, 1, 1), "staff2@hotel.com", false, "staff123", "0923456789", 4, "staff2" },
                    { 5, new DateOnly(1, 1, 1), "customer1@gmail.com", false, "customer123", "0934567890", 5, "customer1" },
                    { 6, new DateOnly(1, 1, 1), "customer2@gmail.com", false, "customer123", "0945678901", 6, "customer2" },
                    { 7, new DateOnly(1, 1, 1), "customer3@gmail.com", false, "customer123", "0956789012", 7, "customer3" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "ID", "BookingId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 5 },
                    { 2, 2, 6 },
                    { 3, 3, 7 }
                });

            migrationBuilder.InsertData(
                table: "Manager",
                columns: new[] { "ID", "UserId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "Picture",
                columns: new[] { "ID", "PictureUrl", "RoomId" },
                values: new object[,]
                {
                    { 1, "https://example.com/room1_1.jpg", 1 },
                    { 2, "https://example.com/room1_2.jpg", 1 },
                    { 3, "https://example.com/room2_1.jpg", 2 },
                    { 4, "https://example.com/room2_2.jpg", 2 },
                    { 5, "https://example.com/room3_1.jpg", 3 },
                    { 6, "https://example.com/room4_1.jpg", 4 },
                    { 7, "https://example.com/room5_1.jpg", 5 }
                });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "ID", "UserId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Attendance",
                columns: new[] { "ID", "ManagerId", "WorkShiftEnd", "WorkShiftStart" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 7, 25, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 25, 8, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 1, new DateTime(2025, 7, 24, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 24, 8, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 1, new DateTime(2025, 7, 24, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 25, 16, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "ID", "CheckInTime", "CheckOutTime", "CustomerId", "ManagerId", "RoomId", "Status", "TotalPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 23, 22, 4, 40, 487, DateTimeKind.Local).AddTicks(3546), null, 1, 1, 5, 2, 1400000m },
                    { 2, new DateTime(2025, 7, 26, 22, 4, 40, 487, DateTimeKind.Local).AddTicks(3558), new DateTime(2025, 7, 28, 22, 4, 40, 487, DateTimeKind.Local).AddTicks(3559), 2, 1, 2, 0, 1600000m },
                    { 3, new DateTime(2025, 7, 20, 22, 4, 40, 487, DateTimeKind.Local).AddTicks(3562), new DateTime(2025, 7, 22, 22, 4, 40, 487, DateTimeKind.Local).AddTicks(3563), 3, 1, 1, 2, 1000000m }
                });

            migrationBuilder.InsertData(
                table: "WorkShift",
                columns: new[] { "ID", "ManagerId", "RoomId", "StaffId", "WorkShipEnd", "WorkShipStart" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 2, 2, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, 3, 1, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 4, 2, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 5, 1, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_ManagerId",
                table: "Attendance",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CustomerId",
                table: "Booking",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ManagerId",
                table: "Booking",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RoomId",
                table: "Booking",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_UserId",
                table: "Manager",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_RoomId",
                table: "Picture",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_UserId",
                table: "Staffs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShift_ManagerId",
                table: "WorkShift",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShift_RoomId",
                table: "WorkShift",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShift_StaffId",
                table: "WorkShift",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "WorkShift");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

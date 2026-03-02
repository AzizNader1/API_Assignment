using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Assignment.Migrations
{
    /// <inheritdoc />
    public partial class addastatuscolumntotheattendancetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendanceStatus",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceStatus",
                table: "Attendances");
        }
    }
}

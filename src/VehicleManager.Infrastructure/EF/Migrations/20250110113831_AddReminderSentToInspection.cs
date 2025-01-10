using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManager.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderSentToInspection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReminderSent",
                table: "Inspections",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderSent",
                table: "Inspections");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManager.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyToInsurance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReminderSent",
                table: "Insurances",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderSent",
                table: "Insurances");
        }
    }
}

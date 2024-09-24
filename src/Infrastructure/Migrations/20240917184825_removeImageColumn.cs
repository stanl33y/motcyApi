using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace motcyApi.Migrations
{
    /// <inheritdoc />
    public partial class removeImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseImage",
                table: "DeliveryPeople");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicenseImage",
                table: "DeliveryPeople",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

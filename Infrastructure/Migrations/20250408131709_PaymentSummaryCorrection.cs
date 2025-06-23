using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PaymentSummaryCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentSummery_Last4",
                table: "Orders",
                newName: "PaymentSummary_Last4");

            migrationBuilder.RenameColumn(
                name: "PaymentSummery_ExpMonth",
                table: "Orders",
                newName: "PaymentSummary_ExpMonth");

            migrationBuilder.RenameColumn(
                name: "PaymentSummery_Brand",
                table: "Orders",
                newName: "PaymentSummary_Brand");

            migrationBuilder.RenameColumn(
                name: "PaymentSummery_Year",
                table: "Orders",
                newName: "PaymentSummary_ExpYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentSummary_Last4",
                table: "Orders",
                newName: "PaymentSummery_Last4");

            migrationBuilder.RenameColumn(
                name: "PaymentSummary_ExpMonth",
                table: "Orders",
                newName: "PaymentSummery_ExpMonth");

            migrationBuilder.RenameColumn(
                name: "PaymentSummary_Brand",
                table: "Orders",
                newName: "PaymentSummery_Brand");

            migrationBuilder.RenameColumn(
                name: "PaymentSummary_ExpYear",
                table: "Orders",
                newName: "PaymentSummery_Year");
        }
    }
}

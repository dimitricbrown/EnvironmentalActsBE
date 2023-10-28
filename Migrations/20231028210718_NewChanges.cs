using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvironmentalActsBE.Migrations
{
    /// <inheritdoc />
    public partial class NewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uid",
                table: "Users",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "Events",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Events",
                newName: "CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Uid",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Clean Up");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedBy",
                value: "");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "Users",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "Events",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Events",
                newName: "categoryId");

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "CleanUp");
        }
    }
}

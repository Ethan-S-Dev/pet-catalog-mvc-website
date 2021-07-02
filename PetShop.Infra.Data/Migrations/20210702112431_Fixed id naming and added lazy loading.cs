using Microsoft.EntityFrameworkCore.Migrations;

namespace PetShop.Infra.Data.Migrations
{
    public partial class Fixedidnamingandaddedlazyloading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Animals",
                newName: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "Animals",
                newName: "Id");
        }
    }
}

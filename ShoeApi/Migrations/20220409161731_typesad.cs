using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class typesad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandType_Brands_BrandId",
                table: "BrandType");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandType_Type_TypeId",
                table: "BrandType");

            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Type_TypeId",
                table: "Goods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrandType",
                table: "BrandType");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "Types");

            migrationBuilder.RenameTable(
                name: "BrandType",
                newName: "BrandTypes");

            migrationBuilder.RenameIndex(
                name: "IX_BrandType_TypeId",
                table: "BrandTypes",
                newName: "IX_BrandTypes_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BrandType_BrandId",
                table: "BrandTypes",
                newName: "IX_BrandTypes_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrandTypes",
                table: "BrandTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandTypes_Brands_BrandId",
                table: "BrandTypes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandTypes_Types_TypeId",
                table: "BrandTypes",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Types_TypeId",
                table: "Goods",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandTypes_Brands_BrandId",
                table: "BrandTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandTypes_Types_TypeId",
                table: "BrandTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Types_TypeId",
                table: "Goods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrandTypes",
                table: "BrandTypes");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Type");

            migrationBuilder.RenameTable(
                name: "BrandTypes",
                newName: "BrandType");

            migrationBuilder.RenameIndex(
                name: "IX_BrandTypes_TypeId",
                table: "BrandType",
                newName: "IX_BrandType_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BrandTypes_BrandId",
                table: "BrandType",
                newName: "IX_BrandType_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrandType",
                table: "BrandType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandType_Brands_BrandId",
                table: "BrandType",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandType_Type_TypeId",
                table: "BrandType",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Type_TypeId",
                table: "Goods",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

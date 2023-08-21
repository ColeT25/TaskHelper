using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskHelperWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAllForiegnKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Boards_ProjectID",
                table: "Boards",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Projects_ProjectID",
                table: "Boards",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Projects_ProjectID",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_ProjectID",
                table: "Boards");
        }
    }
}

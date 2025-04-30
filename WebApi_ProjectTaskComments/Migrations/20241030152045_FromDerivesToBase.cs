using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_ProjectTaskComments.Migrations
{
    public partial class FromDerivesToBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProjectTaskId",
                table: "Comments",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ProjectTasks_ProjectTaskId",
                table: "Comments",
                column: "ProjectTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ProjectTasks_ProjectTaskId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProjectTaskId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "Comments");
        }
    }
}

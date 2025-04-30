using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_ProjectTaskComments.Migrations
{
    public partial class PropertyIsDeletedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectTasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comments");

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
    }
}

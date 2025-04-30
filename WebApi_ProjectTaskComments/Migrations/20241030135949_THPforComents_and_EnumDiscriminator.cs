using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_ProjectTaskComments.Migrations
{
    public partial class THPforComents_and_EnumDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ProjectTasks_ProjectTaskId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProjectId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ProjectTaskId",
                table: "Comments",
                newName: "SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProjectTaskId",
                table: "Comments",
                newName: "IX_Comments_SourceId");

            migrationBuilder.AddColumn<int>(
                name: "SourceType",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Projects_SourceId",
                table: "Comments",
                column: "SourceId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ProjectTasks_SourceId",
                table: "Comments",
                column: "SourceId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Projects_SourceId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ProjectTasks_SourceId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Comments",
                newName: "ProjectTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_SourceId",
                table: "Comments",
                newName: "IX_Comments_ProjectTaskId");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProjectId",
                table: "Comments",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                table: "Comments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ProjectTasks_ProjectTaskId",
                table: "Comments",
                column: "ProjectTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id");
        }
    }
}

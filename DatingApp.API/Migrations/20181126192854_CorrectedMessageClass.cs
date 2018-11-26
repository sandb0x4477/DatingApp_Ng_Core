using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class CorrectedMessageClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_RecipeintId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecipeintId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RecipeintId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "MessageRead",
                table: "Messages",
                newName: "MessageSent");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "MessageSent",
                table: "Messages",
                newName: "MessageRead");

            migrationBuilder.AddColumn<int>(
                name: "RecipeintId",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipeintId",
                table: "Messages",
                column: "RecipeintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_RecipeintId",
                table: "Messages",
                column: "RecipeintId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

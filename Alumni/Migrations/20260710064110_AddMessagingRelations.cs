using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumni.Migrations
{
    /// <inheritdoc />
    public partial class AddMessagingRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Converataions_Messages_LastMessageId",
                table: "Converataions");

            migrationBuilder.DropForeignKey(
                name: "FK_Converataions_Users_User1Id",
                table: "Converataions");

            migrationBuilder.DropForeignKey(
                name: "FK_Converataions_Users_User2Id",
                table: "Converataions");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Converataions_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Converataions",
                table: "Converataions");

            migrationBuilder.DropIndex(
                name: "IX_Converataions_LastMessageId",
                table: "Converataions");

            migrationBuilder.DropIndex(
                name: "IX_Converataions_User1Id",
                table: "Converataions");

            migrationBuilder.RenameTable(
                name: "Converataions",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_Converataions_User2Id",
                table: "Conversations",
                newName: "IX_Conversations_User2Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_LastMessageId",
                table: "Conversations",
                column: "LastMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_User1Id_User2Id",
                table: "Conversations",
                columns: new[] { "User1Id", "User2Id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Messages_LastMessageId",
                table: "Conversations",
                column: "LastMessageId",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_User1Id",
                table: "Conversations",
                column: "User1Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_User2Id",
                table: "Conversations",
                column: "User2Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "ConversationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Messages_LastMessageId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_User1Id",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_User2Id",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_LastMessageId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_User1Id_User2Id",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Converataions");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_User2Id",
                table: "Converataions",
                newName: "IX_Converataions_User2Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Converataions",
                table: "Converataions",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Converataions_LastMessageId",
                table: "Converataions",
                column: "LastMessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Converataions_User1Id",
                table: "Converataions",
                column: "User1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Converataions_Messages_LastMessageId",
                table: "Converataions",
                column: "LastMessageId",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Converataions_Users_User1Id",
                table: "Converataions",
                column: "User1Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Converataions_Users_User2Id",
                table: "Converataions",
                column: "User2Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Converataions_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Converataions",
                principalColumn: "ConversationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

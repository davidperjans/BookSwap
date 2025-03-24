using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSwap.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendshipAndRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_UserId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeRequests_Users_UserId",
                table: "TradeRequests");

            migrationBuilder.DropIndex(
                name: "IX_TradeRequests_UserId",
                table: "TradeRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_UserId",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TradeRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FriendRequests");

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1Id = table.Column<int>(type: "int", nullable: false),
                    User2Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeRequests_ReceiverId",
                table: "TradeRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeRequests_SenderId",
                table: "TradeRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_User1Id",
                table: "Friendships",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_User2Id",
                table: "Friendships",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId",
                table: "Friendships",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeRequests_Users_ReceiverId",
                table: "TradeRequests",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeRequests_Users_SenderId",
                table: "TradeRequests",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeRequests_Users_ReceiverId",
                table: "TradeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeRequests_Users_SenderId",
                table: "TradeRequests");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_TradeRequests_ReceiverId",
                table: "TradeRequests");

            migrationBuilder.DropIndex(
                name: "IX_TradeRequests_SenderId",
                table: "TradeRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TradeRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FriendRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradeRequests_UserId",
                table: "TradeRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_UserId",
                table: "FriendRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_UserId",
                table: "FriendRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeRequests_Users_UserId",
                table: "TradeRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

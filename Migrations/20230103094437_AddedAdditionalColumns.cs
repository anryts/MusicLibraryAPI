using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicLibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdditionalColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_song_songs_song_id",
                table: "user_song");

            migrationBuilder.DropForeignKey(
                name: "fk_user_song_users_user_id",
                table: "user_song");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_song",
                table: "user_song");

            migrationBuilder.RenameTable(
                name: "user_song",
                newName: "user_songs");

            migrationBuilder.RenameIndex(
                name: "ix_user_song_user_id",
                table: "user_songs",
                newName: "ix_user_songs_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_song_song_id",
                table: "user_songs",
                newName: "ix_user_songs_song_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_songs",
                table: "user_songs",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_songs_songs_song_id",
                table: "user_songs",
                column: "song_id",
                principalTable: "songs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_songs_users_user_id",
                table: "user_songs",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_songs_songs_song_id",
                table: "user_songs");

            migrationBuilder.DropForeignKey(
                name: "fk_user_songs_users_user_id",
                table: "user_songs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_songs",
                table: "user_songs");

            migrationBuilder.RenameTable(
                name: "user_songs",
                newName: "user_song");

            migrationBuilder.RenameIndex(
                name: "ix_user_songs_user_id",
                table: "user_song",
                newName: "ix_user_song_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_songs_song_id",
                table: "user_song",
                newName: "ix_user_song_song_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_song",
                table: "user_song",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_song_songs_song_id",
                table: "user_song",
                column: "song_id",
                principalTable: "songs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_song_users_user_id",
                table: "user_song",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

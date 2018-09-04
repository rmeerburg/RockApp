using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rock_app.Migrations
{
    public partial class AlbumArtistBpmNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(name: "Name", table: "Artists", maxLength: 450);
            migrationBuilder.AlterColumn<string>(name: "Name", table: "Albums", maxLength: 450);

            migrationBuilder.AddUniqueConstraint("IX_Unique_Artist_Name", "Artists", "Name");
            migrationBuilder.CreateIndex("IX_Album_Name", "Albums", "Name");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "Bpm",
                table: "Songs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "ArtistId",
                table: "Albums",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint("IX_Unique_Artist_Name", "Artists", "Name");
            migrationBuilder.DropIndex("IX_Album_Name", "Albums", "Name");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "Bpm",
                table: "Songs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ArtistId",
                table: "Albums",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

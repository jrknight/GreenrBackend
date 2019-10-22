using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GreenrAPI.Migrations
{
    public partial class TripStructureChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AspNetUsers_UserId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "tblTrips");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_UserId",
                table: "tblTrips",
                newName: "IX_tblTrips_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblTrips",
                table: "tblTrips",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "tblUserTrips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    TripId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblUserTrips_tblTrips_TripId",
                        column: x => x.TripId,
                        principalTable: "tblTrips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserTrips_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserTrips_TripId",
                table: "tblUserTrips",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserTrips_UserId",
                table: "tblUserTrips",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblTrips_AspNetUsers_UserId",
                table: "tblTrips",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblTrips_AspNetUsers_UserId",
                table: "tblTrips");

            migrationBuilder.DropTable(
                name: "tblUserTrips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblTrips",
                table: "tblTrips");

            migrationBuilder.RenameTable(
                name: "tblTrips",
                newName: "Trips");

            migrationBuilder.RenameIndex(
                name: "IX_tblTrips_UserId",
                table: "Trips",
                newName: "IX_Trips_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AspNetUsers_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

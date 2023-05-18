using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfilesAPI.Persistence.Migrations
{
    public partial class CreateReceptionist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receptionists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Office_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptionists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptionists_HumansInfo_InfoId",
                        column: x => x.InfoId,
                        principalTable: "HumansInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_InfoId",
                table: "Receptionists",
                column: "InfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receptionists");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagmentDAL.Migrations
{
    /// <inheritdoc />
    public partial class Updating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_Members_MemberId1",
                table: "MemberShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_MemberId1",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MemberShips");

            migrationBuilder.RenameColumn(
                name: "MemberId1",
                table: "MemberShips",
                newName: "MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips",
                columns: new[] { "MemberId", "PlanId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_Members_MemberId",
                table: "MemberShips",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_Members_MemberId",
                table: "MemberShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "MemberShips",
                newName: "MemberId1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MemberShips",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_MemberId1",
                table: "MemberShips",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_Members_MemberId1",
                table: "MemberShips",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

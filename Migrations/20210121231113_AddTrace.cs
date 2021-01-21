using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.NET_CORE.Migrations
{
    public partial class AddTrace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TraceStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TraceEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracePoints = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trace", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trace");
        }
    }
}

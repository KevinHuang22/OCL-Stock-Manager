using Microsoft.EntityFrameworkCore.Migrations;

namespace OCL_Apis.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Order = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Batch = table.Column<string>(nullable: true),
                    Tinplate = table.Column<int>(nullable: false),
                    Rejection = table.Column<int>(nullable: false),
                    Good = table.Column<int>(nullable: false),
                    Bad = table.Column<int>(nullable: false),
                    CanType = table.Column<int>(nullable: false),
                    ResourceStatus = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}

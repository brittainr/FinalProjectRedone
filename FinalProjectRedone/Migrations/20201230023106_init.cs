using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectRedone.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Finances",
                columns: table => new
                {
                    TaxID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: true),
                    Month = table.Column<string>(nullable: true),
                    Medicare = table.Column<double>(nullable: false),
                    SocialSecurity = table.Column<double>(nullable: false),
                    TaxedIncome = table.Column<int>(nullable: false),
                    MonthlyIncome = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finances", x => x.TaxID);
                    table.ForeignKey(
                        name: "FK_Finances_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    BudgetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetItem = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    UserID = table.Column<int>(nullable: true),
                    TaxModelTaxID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.BudgetID);
                    table.ForeignKey(
                        name: "FK_Budget_Finances_TaxModelTaxID",
                        column: x => x.TaxModelTaxID,
                        principalTable: "Finances",
                        principalColumn: "TaxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Budget_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budget_TaxModelTaxID",
                table: "Budget",
                column: "TaxModelTaxID");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_UserID",
                table: "Budget",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Finances_UserID",
                table: "Finances",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropTable(
                name: "Finances");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

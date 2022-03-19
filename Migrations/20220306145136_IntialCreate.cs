using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    Sum = table.Column<int>(type: "INTEGER", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: true),
                    AddressID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID");
                });
  
        
        migrationBuilder.CreateTable(
     name: "Products",
     columns: table => new
     {
         ProductID = table.Column<int>(type: "INTEGER", nullable: false)
             .Annotation("Sqlite:Autoincrement", true),
         Title = table.Column<string>(type: "TEXT", nullable: true),
         Description = table.Column<string>(type: "TEXT", nullable: true),
         ImagePath = table.Column<string>(type: "TEXT", nullable: true),
         Price = table.Column<int>(type: "INTEGER", nullable: false),
         Amount = table.Column<int>(type: "INTEGER", nullable: false)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_Products", x => x.ProductID);
     });
            migrationBuilder.CreateTable(
     name: "Users",
     columns: table => new
     {
         UserID = table.Column<int>(type: "INTEGER", nullable: false)
             .Annotation("Sqlite:Autoincrement", true),
         Username = table.Column<string>(type: "TEXT", nullable: true),
         Password = table.Column<string>(type: "TEXT", nullable: true)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_Users", x => x.UserID);
     });

            migrationBuilder.CreateTable(
             name: "Addresses",
             columns: table => new
             {
                 AddressID = table.Column<int>(type: "INTEGER", nullable: false)
                     .Annotation("Sqlite:Autoincrement", true),
                 Country = table.Column<string>(type: "TEXT", nullable: true),
                 City = table.Column<string>(type: "TEXT", nullable: true),
                 Street = table.Column<string>(type: "TEXT", nullable: true),
                 Zipcode = table.Column<string>(type: "TEXT", nullable: true),
                 Phone = table.Column<string>(type: "TEXT", nullable: true),
                 Email = table.Column<string>(type: "TEXT", nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Addresses", x => x.AddressID);
             });          
            migrationBuilder.CreateIndex(
           name: "IX_Orders_AddressID",
           table: "Orders",
           column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductID",
                table: "Orders",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
            migrationBuilder.DropTable(
               name: "Products");
            migrationBuilder.DropTable(
        name: "Users");
            migrationBuilder.DropTable(
     name: "Addresses");
        }
    }
}

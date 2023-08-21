using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_Inventario.Migrations
{
    /// <inheritdoc />
    public partial class intialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoIdentificacion = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia = table.Column<int>(type: "int", nullable: false),
                    Canton = table.Column<int>(type: "int", nullable: false),
                    Distrito = table.Column<int>(type: "int", nullable: false),
                    Barrio = table.Column<int>(type: "int", nullable: false),
                    OtraDireccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbProvincias",
                columns: table => new
                {
                    Cod = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProvincias", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreProducto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Costo = table.Column<float>(type: "real", nullable: false),
                    Utilidad = table.Column<float>(type: "real", nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioVenta = table.Column<float>(type: "real", nullable: false),
                    Descuento = table.Column<float>(type: "real", nullable: false),
                    CantidadActual = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productos_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreditoMaximo = table.Column<float>(type: "real", nullable: false),
                    cantidadDiasCredito = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Personas_Id",
                        column: x => x.Id,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbCantones",
                columns: table => new
                {
                    ProvinciaID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Canton = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tbProvinciaCod = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCantones", x => new { x.ProvinciaID, x.Canton });
                    table.ForeignKey(
                        name: "FK_tbCantones_tbProvincias_tbProvinciaCod",
                        column: x => x.tbProvinciaCod,
                        principalTable: "tbProvincias",
                        principalColumn: "Cod");
                });

            migrationBuilder.CreateTable(
                name: "tbDistritos",
                columns: table => new
                {
                    ProvinciaID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CantonID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Distrito = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbDistritos", x => new { x.ProvinciaID, x.CantonID, x.Distrito });
                    table.ForeignKey(
                        name: "FK_tbDistritos_tbCantones_ProvinciaID_CantonID",
                        columns: x => new { x.ProvinciaID, x.CantonID },
                        principalTable: "tbCantones",
                        principalColumns: new[] { "ProvinciaID", "Canton" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbBarrios",
                columns: table => new
                {
                    ProvinciaID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CantonID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DistritoID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Barrio = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbBarrios", x => new { x.ProvinciaID, x.CantonID, x.DistritoID, x.Barrio });
                    table.ForeignKey(
                        name: "FK_tbBarrios_tbDistritos_ProvinciaID_CantonID_DistritoID",
                        columns: x => new { x.ProvinciaID, x.CantonID, x.DistritoID },
                        principalTable: "tbDistritos",
                        principalColumns: new[] { "ProvinciaID", "CantonID", "Distrito" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productos_CategoriaId",
                table: "productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbCantones_tbProvinciaCod",
                table: "tbCantones",
                column: "tbProvinciaCod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "tbBarrios");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "tbDistritos");

            migrationBuilder.DropTable(
                name: "tbCantones");

            migrationBuilder.DropTable(
                name: "tbProvincias");
        }
    }
}

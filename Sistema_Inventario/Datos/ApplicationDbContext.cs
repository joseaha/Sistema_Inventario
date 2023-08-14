using Microsoft.EntityFrameworkCore;

namespace Sistema_Inventario.Datos
{
    public class ApplicationDbContext
    {
        public class ApplicationDbContexto : DbContext
        {
            public ApplicationDbContexto(DbContextOptions<ApplicationDbContexto> options) : base(options)
            {
            }
        public DbSet<Sistema_Inventario.Models.CategoriaModel> categoriaModels { get; set; }
        public DbSet<Sistema_Inventario.Models.ProductoModel> productoModels { get; set; }
        }
    }
}

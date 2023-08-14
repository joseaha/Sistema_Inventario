using System.ComponentModel.DataAnnotations;

namespace Sistema_Inventario.Models
{
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de categoria obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatorio.")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public ICollection<ProductoModel> Productos { get; set; }
    }
}

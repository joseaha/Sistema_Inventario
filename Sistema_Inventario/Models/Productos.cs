using System.ComponentModel.DataAnnotations;

namespace Sistema_Inventario.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El código de producto es obligatorio.")]
        [Display(Name = "Código del Producto")]
        public string CodigoProducto { get; set; }

        [StringLength(255)]
        [Display(Name = "Nombre del Producto")]
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "El costo del producto es obligatorio.")]
        [Range(0.01, float.MaxValue, ErrorMessage = "El costo debe ser mayor que cero.")]
        public float Costo { get; set; }

        [Required(ErrorMessage = "La utilidad del producto es obligatoria.")]
        [Range(0.01, float.MaxValue, ErrorMessage = "La utilidad debe ser mayor que cero.")]
        public float Utilidad { get; set; }

        [Required(ErrorMessage = "El impuesto es requerido.")]
        [Range(0.01, float.MaxValue, ErrorMessage = "El impuesto debe estar entre 0 y 100.")]
        public decimal Impuesto { get; set; }

        [Required(ErrorMessage = "El precio venta del producto es obligatorio.")]
        [Display(Name = "Precio venta del Producto")]
        [Range(0.01, float.MaxValue, ErrorMessage = "El precio de venta debe ser mayor que cero.")]
        public float PrecioVenta { get; set; }

        [Required(ErrorMessage = "El descuento del producto es obligatorio.")]
        [Range(0, float.MaxValue, ErrorMessage = "El descuento debe ser mayor o igual a cero.")]
        public float Descuento { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Display(Name = "Cantidad del Producto")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad actual debe ser mayor o igual a cero.")]
        public int CantidadActual { get; set; }

        public bool Estado { get; set; }

        [Display(Name = "Nombre de la Categoría")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}

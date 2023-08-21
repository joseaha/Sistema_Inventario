using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Inventario.Models
{
    public class tbDistrito
    {
        [Key]
        [Column(Order = 1)]
        public string ProvinciaID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string CantonID { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Distrito { get; set; }

        public string Nombre { get; set; }

        [ForeignKey("CantonID, Distrito")] // Definir la relación de clave foránea compuesta
        public tbCanton Canton { get; set; }

        public ICollection<tbBarrios> Barrios { get; set; }
    }
}

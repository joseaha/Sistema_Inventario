using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Inventario.Models
{
    public class tbBarrios
    {
        [Key]
        [Column(Order = 1)]
        public string ProvinciaID { get; set; }

        [Key]
        [Column(Order = 2)]
        public string CantonID { get; set; }

        [Key]
        [Column(Order = 3)]
        public string DistritoID { get; set; }

        [Key]
        [Column(Order = 4)]
        public string Barrio { get; set; }

        public string Nombre { get; set; }

        [ForeignKey("ProvinciaID, CantonID, DistritoID")]
        public tbDistrito Distrito { get; set; }

    }
}

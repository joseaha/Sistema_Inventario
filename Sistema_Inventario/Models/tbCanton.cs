using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Inventario.Models
{
    public class tbCanton
    {
        [Key]
        [Column(Order = 1)]
        public string ProvinciaID { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Canton { get; set; }

        public string Nombre { get; set; }

        public ICollection<tbDistrito> Distritos { get; set; }

    }
}

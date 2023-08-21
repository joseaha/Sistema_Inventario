using System.ComponentModel.DataAnnotations;

namespace Sistema_Inventario.Models
{
    public class tbProvincia
    {
        [Key]
        public string Cod { get; set; }
        public string Nombre { get; set; }

        public ICollection<tbCanton> Cantones { get; set; }

    }
}

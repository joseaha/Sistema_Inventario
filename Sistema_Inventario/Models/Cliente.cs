using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Inventario.Models
{
    public class Cliente : Persona
    {
        [Required]
        [DisplayName("Credito Maximo")]
        public float CreditoMaximo { get; set; }

        [Required]
        [DisplayName("Credito en dias")]
        public int cantidadDiasCredito { get; set; }

        public bool estado { get; set; }
    }
}

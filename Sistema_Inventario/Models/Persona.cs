using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Inventario.Models
{
    public enum TipoCedula
    {
        Dimex=1,
        Juridica=2,
        Nacional=3
    }

    public enum SexoEnum
    {
        Femenino = 1,
        Masculino = 2,
        Otro = 3
    }

    public class Persona
    {
        //---------------------------------------------------------------------------//
        [DisplayName("Numero Identificacion")]
        [Required(ErrorMessage = "El número de identificación es obligatorio.")]
        [Key]
        public string Id { get; set; }
        //---------------------------------------------------------------------------//
        [DisplayName("Tipo Identificacion")]
        [Required(ErrorMessage = "El tipo de identificación es obligatorio.")]
        public TipoCedula TipoIdentificacion { get; set; }
        //---------------------------------------------------------------------------//
        [DisplayName("Nombre completo")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }
        //---------------------------------------------------------------------------//
        [DisplayName("Primer apellido")]
        public string? PrimerApellido { get; set; }
        //---------------------------------------------------------------------------//
        [DisplayName("Segundo apellido")]
        public string? SegundoApellido { get; set; }
        //---------------------------------------------------------------------------//
        public SexoEnum Sexo { get; set; }
        //---------------------------------------------------------------------------//
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; }
        //---------------------------------------------------------------------------//
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [DisplayName("Correo Electronico")]
        public string Correo { get; set; }
        //---------------------------------------------------------------------------//
        [Required(ErrorMessage = "Debe seleccionar una provincia")]
        public int Provincia { get; set; }
        //---------------------------------------------------------------------------//
        [Required(ErrorMessage = "Debe seleccionar un canton")]
        public int Canton { get; set; }
        //---------------------------------------------------------------------------//
        [Required(ErrorMessage = "Debe seleccionar un distrito")]
        public int Distrito { get; set; }
        //---------------------------------------------------------------------------//
        [Required(ErrorMessage = "Debe seleccionar un barrio")]
        public int Barrio { get; set; }
        //---------------------------------------------------------------------------//
        [Required(ErrorMessage = "Ingrese otra referencia de su dirreccion")]
        [DisplayName("Otra referencia")]
        public string? OtraDireccion { get; set; }
        //---------------------------------------------------------------------------//

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace practica4.Models
{
    [Table("t_contacto")]
    public class Contacto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombres son obligatorios.")]
        public string? Nombres { get; set; }
        [NotNull]
        public string? Email { get; set; }
        [NotNull]
        public string? Direccion { get; set; }
        [NotNull]
        public string? Telefono { get; set; }
        
        [NotNull]
        public string? Mensaje { get; set; }

        public string? Etiqueta { get; set; }

        public float Puntuacion { get; set; }
    }
}
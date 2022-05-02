using System;
using System.ComponentModel.DataAnnotations;

namespace Common.BL
{
    public class Parametro
    {
        [Key]
        public int Id { get; set; } //Clave primaria
        public string Llave { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
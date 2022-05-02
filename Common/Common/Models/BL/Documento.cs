using System;
using System.ComponentModel.DataAnnotations;

namespace Common.BL
{
    public class Documento
    {
        [Key]
        public int Id { get; set; } //Clave primaria
        public string Rut { get; set; }
        public string Razon { get; set; }
        public int Folio { get; set; }
        public int TipoDocumento { get; set; }
        public string Data { get; set; }
        public Estado Estado { get; set; }
        public string Error { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
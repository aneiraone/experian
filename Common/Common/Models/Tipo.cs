﻿using Newtonsoft.Json.Linq;

namespace Common
{
    public class Tipo
    {
        public Tipo(string tipoDTE, string rut, string razon, int folio, string mensaje, JArray result)
        {
            TipoDocumento = tipoDTE;
            Rut = rut;
            RazonSocial = razon;
            Folio = folio.ToString();
            Result = result;
        }
        public string TipoDocumento { get; set; }
        public string Rut { get; set; }
        public string RazonSocial { get; set; }
        public string Folio { get; set; }
        public JArray Result { get; set; }
    }
}

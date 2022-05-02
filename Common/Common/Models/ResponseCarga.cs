using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace Common
{
    public class ResponseCarga
    {
        public static readonly string statusOK = "OK";
        public ResponseCarga(string tipoDTE, string rut, string razon, int folio, string mensaje, JArray result = null)
        {
            Status = statusOK;
            Value = new Tipo(tipoDTE, rut, razon, folio, mensaje, result);
        }
        public string Status { get; set; }
        public Tipo Value { get; set; }
    }
}
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

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

#region "VALUE"
public class Tipo
{
    public Tipo(string tipoDTE, string rut, string razon, int folio, string mensaje, JArray result)
    {
        TipoDocumento = tipoDTE;
        Rut = rut;
        RazonSocial = razon;
        Folio = folio.ToString();
        Result = result; //new List<Result>();
      //  Result.Add(new Result(mensaje, mensaje, folio));
    }
    public string TipoDocumento { get; set; }
    public string Rut { get; set; }
    public string RazonSocial { get; set; }
    public string Folio { get; set; }
    public JArray Result { get; set; }
    //public List<Result> Result { get; set; }
}
#endregion

#region "RESULT"
public class Result
{
    public Result(string propiedad, string message, int value)
    {
        Property = propiedad;
        Message = message;
        Value = value;
    }
    public string Property { get; set; }
    public string Message { get; set; }
    public int Value { get; set; }
}
#endregion
using Newtonsoft.Json.Linq;

class RequestCargaDTE
{
    public RequestCargaDTE(JObject json)
    {
        AsignacionFolio = "SI";
        VersionDocumento = "F22";
        TipoContenido = "json";
        Carga = "NO";
        Documento = json;
    }

    public string AsignacionFolio { get; set; }
    public string VersionDocumento { get; set; }
    public string TipoContenido { get; set; }
    public string Carga { get; set; }
    public JObject Documento { get; set; }
   // public Documento Documento { get; }
}

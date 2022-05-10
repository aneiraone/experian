using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Validate : IValidate
{
    
    public readonly string _documento = "Documento";
    public readonly string _encabezado = "Encabezado";
    public readonly string _receptor = "Receptor";
    public readonly string _rut = "RUT";
    public readonly string _razon = "RazonSocial";
    public readonly string _tipoDocumento = "TipoDocumento";
    public readonly string _folio = "Folio";

    public readonly string payload = "payload";
    public readonly string status = "Status";

    public bool ResponseData(dynamic response)
    {
        if (!response.ContainsKey(payload))
        {
            throw new InvalidResponseExperianException();
        }
        return true;
    }

    public bool ResponseCarga(dynamic response)
    {
        if (!response.ContainsKey(status))
        {
            throw new InvalidResponseCargaException(JsonConvert.SerializeObject(response));
        }
        return true;
    }

    public bool RequestDocument(JObject request)
    {
        if (!request.ContainsKey(_documento))
        {
            throw new InvalidRequestException(_documento);
        }
        JObject documento = (JObject)request[_documento];

        if (!documento.ContainsKey(_encabezado))
        {
            throw new InvalidRequestException(_encabezado);
        }

        JObject encabezado = (JObject)documento[_encabezado];
        if (!encabezado.ContainsKey(_receptor))
        {
            throw new InvalidRequestException(_receptor);
        }

        JObject receptor = (JObject)encabezado[_receptor];
        if (!receptor.ContainsKey(_rut))
        {
            throw new InvalidRequestException(_rut);
        }
        if (!receptor.ContainsKey(_razon))
        {
            throw new InvalidRequestException(_razon);
        }

        if (!encabezado.ContainsKey(_tipoDocumento))
        {
            throw new InvalidRequestException(_tipoDocumento);
        }

        if (!encabezado.ContainsKey(_folio))
        {
            throw new InvalidRequestException(_folio);
        }

        return true;
    }
}

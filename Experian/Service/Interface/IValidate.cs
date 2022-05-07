using Newtonsoft.Json.Linq;

interface IValidate
{
    bool RequestDocument(JObject request);
    bool ResponseData(dynamic response);
    bool ResponseCarga(dynamic response);
}

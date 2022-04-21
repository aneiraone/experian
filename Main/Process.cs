using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

class Process
{
    Email email = new Email();
    Serilog.Core.Logger _log = Logger.GetInstance()._Logger;
    public void Execute()
    {
        try
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true).Build();
            AppSettings appSettings = AppSettings.GetInstance();
            config.Bind("AppSettings", appSettings);
           // config.Bind("AppSettings", appSettings); FALTA BINDING DE BASE

            /*DataConnect data = new DataConnect();
            DirectorysFiles DirectoyInfo = data.GetData();
            //Validate info
            if (!DirectoyInfo.Validate(DirectoyInfo))
            {
                log.Generar(this, new Exception(Constants.ExceptionMessage.DIRECTORIO_EMPTY));
                Console.WriteLine(Constants.ExceptionMessage.DIRECTORIO_EMPTY);
                return;
            }
            */
            // string[] fileEntries = null; //= Directory.GetFiles(DirectoyInfo.InDirectory);

            /*if (fileEntries.Length == 0)
            {
                Console.WriteLine(Constants.ConsoleMessage.SIN_DATA);
                return;
            }*/

            ExperianServices experianServices = new ExperianServices();
            string resp = experianServices.Data();
            dynamic jsonResp = JsonConvert.DeserializeObject(resp);
            if (!jsonResp.ContainsKey("data"))
            {
                //thown
            }
            JArray data = jsonResp["data"];

            JWTServices wsServices = new JWTServices();
            Token token = wsServices.GenerateToken();

            _log.Information(Constants.ConsoleMessage.ARCHIVOS_START);
            Serilog.Core.Logger _logFile = Logger.GetInstance()._LoggerFile;

            foreach (JObject item in data)
            {
                //RENUEVA TOKEN SI EXPIRO
                if (!token.ValidateToken(token))
                { token = wsServices.GenerateToken(); }

                string dte = (string)item["Encabezado"]["TipoDocumento"];
                string rut = (string)item["Encabezado"]["Receptor"]["RUT"];
                string razon = (string)item["Encabezado"]["Receptor"]["RazonSocial"];
                int folio = int.Parse((string)item["Encabezado"]["FolioCliente"]);

                ResponseCarga response = new ResponseCarga(dte, rut, razon, folio, string.Empty);
                try
                {
                    dynamic responseCarga = JsonConvert.DeserializeObject(wsServices.Carga(token, item));
                    try
                    {
                        if (!responseCarga.ContainsKey("Status"))
                        {
                            //thown
                        }

                        JArray result = responseCarga.Value.Result;
                        response.Value.Result = result;
                        response.Status = (string)responseCarga.Status;

                        //registro.Generar(Formater(response));
                    }
                    catch
                    {
                        // HAY QUE REVISAR ESO


                        /* JObject value = responseCarga.Value; //json.Value.Result.ToString();
                         JToken propertyValue = value.Property("Result");
                         string mensajeLog = string.Empty;
                         string propertiesLog = string.Empty;

                         if (propertyValue.First.Type == JTokenType.Array)
                         {
                             JArray items = (JArray)propertyValue.First;
                             mensajeLog = items[0]["Message"].ToString();
                             propertiesLog = items[0]["Property"].ToString();
                         }
                         else
                         {
                             mensajeLog = value.Property("Result").Value.ToString();
                             propertiesLog = value.Property("Result").Value.ToString();
                         }
                        */
                        //    response.Value.Result[0].Message = mensajeLog;
                        //   response.Value.Result[0].Property = propertiesLog;
                    }
                    //response.Status = responseCarga.Status;
                    //registro.Generar(Path.GetFileNameWithoutExtension(PathFile) + ExtensioLog, DirectoyInfo.LogDirectory, Formater(response));
                }
                catch (Exception ex)
                {
                    response.Status = "NOK";
                    response.Value.Result.Add(new JObject { { "Message", ex.Message }, { "Property", ex.Message } });
                    _log.Error(Constants.ExceptionMessage.EXCEPTION + ex.Message);

                }

                if (response.Status == ResponseCarga.statusOK)
                {
                    _logFile.Information(Formater(response));
                }
                else
                {
                    _logFile.Error(Formater(response));
                }

                //email.Send(response);
            }
            _log.Information(Constants.ConsoleMessage.ARCHIVOS_END);
        }
        catch (Exception ex)
        {
            if (ex.InnerException is TaskCanceledException)
            {//time out
                _log.Error(Constants.ExceptionMessage.TIMEOUT);
                //email.Send(Constants.ExceptionMessage.TIMEOUT);
                return;
            }
            _log.Error(ex.Message);
            //email.Send(ex.Message);
        }

    }

    private string Formater(ResponseCarga response)
    {
        return JsonConvert.SerializeObject(response);//JValue.Parse(JsonConvert.SerializeObject(response)).ToString(Formatting.Indented);
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

class Process
{
    
    //readonly string PreName = "E0";
    public void Execute()
    {
        LogError log = new LogError();
        try
        {
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
            if (!jsonResp.ContainsKey("data")){ 
                //thown
            }
            JArray data = jsonResp["data"];

            JWTServices wsServices = new JWTServices();
            Token token = wsServices.GenerateToken();
            LogProcess registro = new LogProcess();
            Console.WriteLine(Constants.ConsoleMessage.ARCHIVOS_START);
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
                    //string[] archivoText = File.ReadAllLines(PathFile);
                    // response = GetValuesFiles(archivoText);
                    // string base64 = Base64Encode(File.ReadAllText(PathFile));
                    //  string base64 = Base64Encode(archivoText);
                    /*string pathMove = DirectoyInfo.MoveDirectory + @"\" + Path.GetFileName(PathFile);
                    if (File.Exists(pathMove))
                    {
                        File.Delete(pathMove);
                    }
                    Directory.Move(PathFile, pathMove);
                    */
                    //JsonConvert.SerializeObject(item)
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
                        registro.Generar(Formater(response));

                       // value.Property("Result").Value


                        //  string mensaje = (string)responseCarga.Value;
                        //  response.Value.Result[0].Message = mensaje;
                        //  response.Value.Result[0].Property = mensaje;

                        /* if (mensaje.ToLower().Contains("error leyendo contenido del documento en base64"))
                         {
                             response.Value.TipoDocumento = string.Empty;
                             response.Value.Rut = string.Empty;
                             response.Value.Folio = string.Empty;
                             response.Value.RazonSocial = string.Empty;
                            // response.Value.Result[0].Property = string.Join(" ", mensaje, PathFile);
                         //    response.Value.Result[0].Value = 0;
                         }*/
                    }
                    catch
                    {
                        JObject value = responseCarga.Value; //json.Value.Result.ToString();
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
                    //    response.Value.Result[0].Message = mensajeLog;
                     //   response.Value.Result[0].Property = propertiesLog;
                    }
                    response.Status = responseCarga.Status;
                    //registro.Generar(Path.GetFileNameWithoutExtension(PathFile) + ExtensioLog, DirectoyInfo.LogDirectory, Formater(response));
                }
                catch (Exception ex)
                {
                   // response.Value.Result[0].Message = ex.Message;
                   // response.Value.Result[0].Property = ex.Message;
                    response.Status = "NOK";
                    //registro.Generar(Path.GetFileNameWithoutExtension(PathFile) + ExtensioLog, DirectoyInfo.LogDirectory, Formater(response));
                    Console.WriteLine(Constants.ExceptionMessage.EXCEPTION + ex.Message);

                    /*if (File.Exists(PathFile))
                    {
                        string pathMove = DirectoyInfo.MoveDirectory + @"\" + Path.GetFileName(PathFile);
                        if (File.Exists(pathMove))
                        {
                            File.Delete(pathMove);
                        }
                        Directory.Move(PathFile, pathMove);
                    }*/
                }
            }
            Console.WriteLine(Constants.ConsoleMessage.ARCHIVOS_END);
        }
        catch (Exception ex)
        {
            if (ex.InnerException is TaskCanceledException)
            {//time out
                log.Generar(this, new Exception(Constants.ExceptionMessage.TIMEOUT));
                Console.WriteLine(Constants.ExceptionMessage.TIMEOUT);
                return;
            }
            log.Generar(this, ex);
            Console.WriteLine(ex.Message);
        }

    }

  /*  private string Base64Encode(string text)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(plainTextBytes);
    }
  */
    private string Formater(ResponseCarga response)
    {
        return JValue.Parse(JsonConvert.SerializeObject(response)).ToString(Formatting.Indented);
    }

   /* private ResponseCarga GetValuesFiles(string[] text)
    {
        int folio = 0;
        string razon = string.Empty, rut = string.Empty, tipoDTE = string.Empty;
        bool procesado = false;
        char[] charSeparators = new char[] { ';' };
        foreach (string s in text)
        {
            string[] data = s.Split(charSeparators);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].ToUpper() == "A")
                {
                    tipoDTE = data[1];
                    folio = Int32.Parse(data[3]);
                    rut = data[18];
                    razon = data[19];
                    procesado = true;
                    break;
                }
            }

            if (procesado) { break; }
        }
        return new ResponseCarga(tipoDTE, rut, razon, folio, string.Empty);
    }
   */
}
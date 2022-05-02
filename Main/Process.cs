using Common;
using Common.BL;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

class Process
{
    private Email email = new Email();
    private Serilog.Core.Logger _log = Logger.GetInstance()._Logger;
    private ExperianCore.ExperianDBContext _context = new ExperianCore.ExperianDBContext();
    private Validate validate = new Validate();

    private readonly string cargaOK = "Carga exitosa";
    public void Execute()
    {
        try
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true).Build();
            AppSettings appSettings = AppSettings.GetInstance();
            config.Bind("AppSettings", appSettings);

            #region "BIND PARAMETERS"
            List<Parametro> _unSetParam = _context.Parametro.Where(d => d.Activo == true).ToList();
            Parametros parametros = new Parametros(_unSetParam);
            #endregion

            ExperianServices experianServices = new ExperianServices();
            string resp = experianServices.Data();
            dynamic jsonResp = JsonConvert.DeserializeObject(resp);
            validate.ResponseData(jsonResp);

            JArray data = jsonResp[validate.payload];
            JWTServices wsServices = new JWTServices();
            Token token = wsServices.GenerateToken();

            _log.Information(Constants.ConsoleMessage.ARCHIVOS_START);
            Serilog.Core.Logger _logFile = Logger.GetInstance()._LoggerFile;

            foreach (JObject item in data)
            {
                try
                {
                    validate.RequestDocument(item);
                    string dte = (string)item[validate._encabezado][validate._tipoDocumento];
                    string rut = (string)item[validate._encabezado][validate._receptor][validate._rut];
                    string razon = (string)item[validate._encabezado][validate._receptor][validate._razon];
                    int folio = int.Parse((string)item[validate._encabezado][validate._folio]);
                    SaveNewDocument(rut, int.Parse(dte), folio, JsonConvert.SerializeObject(item)); //ADD DOCUMENT DB
                }
                catch
                {

                }
            }

            List<Documento> _documents = _context.Documento.Where(d => d.Estado == Estado.Pendiente).OrderByDescending(d => d.FechaCreacion).ToList();
            foreach (Documento document in _documents)
            {
                try
                {
                    // RENUEVA TOKEN SI EXPIRO
                    if (!token.ValidateToken(token))
                    { token = wsServices.GenerateToken(); }

                    ResponseCarga response = new ResponseCarga(
                        document.TipoDocumento.ToString(),
                        document.Rut,
                        document.Razon, //razon
                        document.Folio,
                        string.Empty
                    );
                    JObject request = JObject.Parse(document.Data);
                    dynamic responseCarga = JsonConvert.DeserializeObject(wsServices.Carga(token, request));
                    validate.ResponseCarga(responseCarga);
                    response.Status = (string)responseCarga.Status;
                    if (response.Status == ResponseCarga.statusOK)
                    {
                        response.Value.Result.Add(new JObject { { "Message", cargaOK }, { "Property", cargaOK } });
                        _logFile.Information(Formater(response));
                    }
                    else
                    {
                        JArray result = responseCarga.Value.Result;
                        response.Value.Result = result;
                        _logFile.Error(Formater(response));
                    }
                    UpdateDocument(document.Id, Estado.Enviado, JsonConvert.SerializeObject(response.Value.Result));
                }
                catch (Exception ex)
                {
                    #region "TIMEOUT"
                    bool timeOut = false;
                    if (ex.InnerException is HttpRequestException)
                    {
                        ex = ex.InnerException;
                        SocketError erro = ((System.Net.Sockets.SocketException)ex.GetBaseException()).SocketErrorCode;
                        if (erro == SocketError.TimedOut)
                        {
                            timeOut = true;
                        }
                    }

                    if (ex.InnerException is TaskCanceledException)
                    {
                        timeOut = true;
                    }

                    if (timeOut)
                    {
                        _log.Error(Constants.ExceptionMessage.TIMEOUT);
                    }
                    #endregion
                    _log.Error(Constants.ExceptionMessage.EXCEPTION + ex.Message);
                }
            }
            _log.Information(Constants.ConsoleMessage.ARCHIVOS_END);

            //foreach (JObject item in data)
            //{
            //    try
            //    {
            //        validate.RequestDocument(item);

            //        string dte = (string)item[validate._encabezado][validate._tipoDocumento];
            //        string rut = (string)item[validate._encabezado][validate._receptor][validate._rut];
            //        string razon = (string)item[validate._encabezado][validate._receptor][validate._razon];
            //        int folio = int.Parse((string)item[validate._encabezado][validate._folio]);

            //        int idDocumento = SaveNewDocument(rut, int.Parse(dte), folio, JsonConvert.SerializeObject(item)); //ADD DOCUMENT DB
            //        ResponseCarga response = new ResponseCarga(dte, rut, razon, folio, string.Empty);
            //        dynamic responseCarga = JsonConvert.DeserializeObject(wsServices.Carga(token, item));

            //        validate.ResponseCarga(responseCarga);
            //        response.Status = (string)responseCarga.Status;
            //        if (response.Status == ResponseCarga.statusOK)
            //        {
            //            response.Value.Result.Add(new JObject { { "Message", cargaOK }, { "Property", cargaOK } });
            //            _logFile.Information(Formater(response));
            //        }
            //        else
            //        {
            //            JArray result = responseCarga.Value.Result;
            //            response.Value.Result = result;
            //            _logFile.Error(Formater(response));
            //        }
            //        UpdateDocument(idDocumento, Estado.Enviado, JsonConvert.SerializeObject(response.Value.Result));
            //        //email.Send(response);

            //        //registro.Generar(Formater(response));

            //        //try
            //        //{




            //        //}
            //        //catch
            //        //{
            //        // HAY QUE REVISAR ESO


            //        /* JObject value = responseCarga.Value; //json.Value.Result.ToString();
            //         JToken propertyValue = value.Property("Result");
            //         string mensajeLog = string.Empty;
            //         string propertiesLog = string.Empty;

            //         if (propertyValue.First.Type == JTokenType.Array)
            //         {
            //             JArray items = (JArray)propertyValue.First;
            //             mensajeLog = items[0]["Message"].ToString();
            //             propertiesLog = items[0]["Property"].ToString();
            //         }
            //         else
            //         {
            //             mensajeLog = value.Property("Result").Value.ToString();
            //             propertiesLog = value.Property("Result").Value.ToString();
            //         }
            //        */
            //        //    response.Value.Result[0].Message = mensajeLog;
            //        //   response.Value.Result[0].Property = propertiesLog;
            //        // }
            //        //response.Status = responseCarga.Status;
            //        //registro.Generar(Path.GetFileNameWithoutExtension(PathFile) + ExtensioLog, DirectoyInfo.LogDirectory, Formater(response));
            //    }
            //    catch (Exception ex)
            //    {
            //        // response.Status = "NOK";
            //        // response.Value.Result.Add(new JObject { { "Message", ex.Message }, { "Property", ex.Message } });
            //        #region "TIMEOUT"
            //        bool timeOut = false;
            //        if (ex.InnerException is HttpRequestException)
            //        {
            //            ex = ex.InnerException;
            //            SocketError erro = ((System.Net.Sockets.SocketException)ex.GetBaseException()).SocketErrorCode;
            //            if (erro == SocketError.TimedOut)
            //            {
            //                timeOut = true;
            //            }
            //        }

            //        if (ex.InnerException is TaskCanceledException)
            //        {
            //            timeOut = true;
            //        }

            //        if (timeOut)
            //        {
            //            _log.Error(Constants.ExceptionMessage.TIMEOUT);
            //        }
            //        #endregion
            //        _log.Error(Constants.ExceptionMessage.EXCEPTION + ex.Message);
            //    }
            //}

        }
        catch (Exception ex)
        {
            #region "TIMEOUT"
            bool timeOut = false;
            if (ex.InnerException is HttpRequestException)
            {
                ex = ex.InnerException;
                SocketError erro = ((System.Net.Sockets.SocketException)ex.GetBaseException()).SocketErrorCode;
                if (erro == SocketError.TimedOut)
                {
                    timeOut = true;
                }
            }

            if (ex.InnerException is TaskCanceledException)
            {
                timeOut = true;
            }

            if (timeOut)
            {
                _log.Error(Constants.ExceptionMessage.TIMEOUT);
            }
            #endregion  
            _log.Error(ex.Message);
            //email.Send((timeOut ? Constants.ExceptionMessage.TIMEOUT : ex.Message);
        }

    }

    private string Formater(ResponseCarga response)
    {
        return JsonConvert.SerializeObject(response);//JValue.Parse(JsonConvert.SerializeObject(response)).ToString(Formatting.Indented);
    }

    private void SaveNewDocument(string rut, int tipo, int folio, string data)
    {
        Documento documento = new Documento()
        {
            Rut = rut,
            TipoDocumento = tipo,
            Folio = folio,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now,
            Estado = Estado.Pendiente,
            Data = data
        };
        _context.Add(documento);
        _context.SaveChanges();
        //return documento.Id;
    }

    private bool UpdateDocument(int id, Estado estado, string error)
    {
        Documento documento = _context.Documento.First(x => x.Id == id);
        documento.FechaModificacion = DateTime.Now;
        documento.Estado = estado;
        documento.Error = error;
        _context.SaveChanges();
        return true;
    }
}
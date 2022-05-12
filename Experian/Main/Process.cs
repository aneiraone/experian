using Common;
using Common.BL;
using ExperianCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

class Process
{
    private Email email = new Email();
    private Serilog.Core.Logger _log = Logger.GetInstance()._Logger;
    private Serilog.Core.Logger _logData = Logger.GetInstance()._LoggerData;
    private DocumentoService documentsService = new DocumentoService();
    private ParametroService parametersService = new ParametroService();

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
            List<Parametro> _unSetParam = parametersService.Get();
            Parametros parametros = Parametros.GetInstance();
            parametros.LoadParametros(_unSetParam);
            #endregion

            ExperianServices experianServices = new ExperianServices();
            Token tokenExperian = experianServices.GenerateToken();
            dynamic responseData = JsonConvert.DeserializeObject(experianServices.Data(tokenExperian));
            _logData.Information(JsonConvert.SerializeObject(responseData));
            validate.ResponseData(responseData);
            JArray data = responseData[validate.payload];
            _log.Information(Constants.ConsoleMessage.ARCHIVOS_START);
            foreach (JObject item in data)
            {
                try
                {
                    validate.RequestDocument(item);
                    string dte = (string)item[validate._documento][validate._encabezado][validate._tipoDocumento];
                    string rut = (string)item[validate._documento][validate._encabezado][validate._emisor][validate._rut];
                    string razon = (string)item[validate._documento][validate._encabezado][validate._emisor][validate._razon];
                    int folio = int.Parse((string)item[validate._documento][validate._encabezado][validate._folio]);
                    documentsService.Save(rut, razon, int.Parse(dte), folio, JsonConvert.SerializeObject(item));// ADD DOCUMENT DB
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is MySqlException)
                    {
                        ex = ex.InnerException; //TIME OUT MYSQL
                    }
                    _log.Error(ex.Message);
                    //return;
                }
            }

            API2020Services wsServices = new API2020Services();
            Serilog.Core.Logger _logFile = Logger.GetInstance()._LoggerFile;
            //OBTIENE LOS DOCUMENTOS PENDIENTES DESDE LA BASE
            List<Documento> _documents = documentsService.GetPendientes();
            if (_documents.Count == 0)
            {
                _log.Information(Constants.ConsoleMessage.SIN_DATA);
                email.Send(string.Empty, false);
            }
            else
            {
                _log.Information(string.Format(Constants.ConsoleMessage.PROCESAR_DOCUMENTOS, _documents.Count));
                Token token = new Token();
                string rutBefore = string.Empty;
                foreach (Documento document in _documents)
                {
                    try
                    {
                        Parametros.GetInstance().IdentificadorEmpresa = document.Rut.Split("-")[0];
                        ResponseCarga response = new ResponseCarga(
                            document.TipoDocumento.ToString(),
                            document.Rut,
                            document.Razon,
                            document.Folio,
                            string.Empty
                        );

                        if (rutBefore != document.Rut) //GENERA NUEVO TOKEN
                        {token = wsServices.GenerateToken(); }
                        // RENUEVA TOKEN SI EXPIRO
                        if (!token.ValidateToken(token))
                        { token = wsServices.GenerateToken(); }

                        dynamic responseCarga = JsonConvert.DeserializeObject(wsServices.Carga(token, JObject.Parse(document.Data)));
                        validate.ResponseCarga(responseCarga);

                        response.Status = (string)responseCarga.Status;
                        Estado estado = document.Estado;
                        if (response.Status == ResponseCarga.statusOK)
                        {
                            estado = Estado.EnviadoOK;
                            response.Value.Result = new JArray() { new JObject { { "Message", cargaOK }, { "Property", cargaOK } } };
                            _logFile.Information(Formater(response));
                        }
                        else
                        {
                            estado = Estado.EnviadoConError;
                            if (responseCarga.Value.Type == JTokenType.String)
                            {
                                string error = (string)responseCarga.Value;
                                response.Value.Result = new JArray() { new JObject { { "Message", error }, { "Property", error } } };
                            }
                            else
                            {
                                response.Value.Result = responseCarga.Value.Result;
                            }
                            _logFile.Error(Formater(response));
                        }
                        documentsService.Update(document.Id, estado, JsonConvert.SerializeObject(response.Value.Result));
                        email.Send(response);
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException is MySqlException)
                        {
                            ex = ex.InnerException; //TIME OUT MYSQL
                        }
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
                        email.Send(timeOut ? Constants.ExceptionMessage.TIMEOUT : Constants.ExceptionMessage.EXCEPTION + ex.Message, true);
                    }
                }
            }
            _log.Information(Constants.ConsoleMessage.ARCHIVOS_END);

        }
        catch (Exception ex)
        {
            if (ex.InnerException is MySqlException)
            {
                ex = ex.InnerException; //TIME OUT MYSQL
            }
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
            email.Send(timeOut ? Constants.ExceptionMessage.TIMEOUT : ex.Message, true);
        }
    }

    private string Formater(ResponseCarga response)
    {
        return JsonConvert.SerializeObject(response);
    }
}
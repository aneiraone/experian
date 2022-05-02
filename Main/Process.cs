using Common;
using Common.BL;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
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

            //OBTIENE LOS DOCUMENTOS PENDIENTES DESDE LA BASE
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
                    //email.Send(response);
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
                    //email.Send((timeOut ? Constants.ExceptionMessage.TIMEOUT : Constants.ExceptionMessage.EXCEPTION + ex.Message);
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
            //email.Send((timeOut ? Constants.ExceptionMessage.TIMEOUT : ex.Message);
        }
    }

    private string Formater(ResponseCarga response)
    {
        return JsonConvert.SerializeObject(response);
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
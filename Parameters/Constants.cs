using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Constants
{
    public class ConsoleMessage
    {
        public const string START = "Iniciando Proceso";
        public const string FINISH = "Finalizando Proceso";
        public const string ARCHIVOS_START = "Procesando Archivos Inicio...";
        public const string ARCHIVOS_END = "Procesando Archivos Fin...";
        public const string SIN_DATA = "Directorio IN sin Archivos";
    }

    public class ServiceRest
    {
        public const string URL_TOKEN = "dbnetJwt/Login/getToken";
        public const string URL_CARGA = "/wsscargaDte/api/CargaDte";
        public const string ContentType = "application/json";
    }

    public class ExceptionMessage {
        public const string DIRECTORIO_EMPTY = "No se cargo uno de los Directorios";
        public const string EXCEPTION = "Procesando Archivos Error: ";
        public const string TIMEOUT = "TIMEOUT, Se cumplio el tiempo de espera para el servicio";
        public const string URLNOVALIDA = "Error 404 No se pudo retornar respuesta de la url solicitada ";
    }
}



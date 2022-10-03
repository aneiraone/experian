class Constants
{
    public class ConsoleMessage
    {
        public const string START = "Iniciando Proceso";
        public const string FINISH = "Finalizando Proceso";
        public const string ARCHIVOS_START = "Procesando Archivos Inicio...";
        public const string ARCHIVOS_END = "Procesando Archivos Fin...";
        public const string SIN_DATA = "No hay documentos para procesar";
        public const string PROCESAR_DOCUMENTOS = "Existen {0} Documentos para procesar";
        public const string DOCUMENTO_OK = "Documento Enviado sin Errores";
        public const string DOCUMENTO_ERROR = "Documento Enviado con Errores";
        public const string SEND_MAIL = "Generando Envio correo para: {0}";
    }

    public class ServiceRest
    {
        public const string ContentType = "application/json";
    }

    public class ExceptionMessage
    {
        public const string DIRECTORIO_EMPTY = "No se cargo uno de los Directorios";
        public const string EXCEPTION = "Procesando Archivos Error: ";
        public const string TIMEOUT = "TIMEOUT, Se cumplio el tiempo de espera para el servicio";
        public const string URLNOVALIDA = "Error 404 No se pudo retornar respuesta de la url solicitada ";
    }
}
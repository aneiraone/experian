using System;
using System.Configuration;

class Credentials
{
    public ServicesToken GetToken { get; set; }
    public class ServicesToken
    {
        public string IdentificadorEmpresa { get; set; } = ConfigurationManager.AppSettings.Get("IdentificadorEmpresa");
        public string UsuarioEmpresa { get; set; } = ConfigurationManager.AppSettings.Get("UsuarioEmpresa");
        public string AutorizacionEmpresa { get; set; } = ConfigurationManager.AppSettings.Get("AutorizacionEmpresa");
    }

    public class ResponseServicesToken
    {
        public string Token { get; set; }
    }
}
namespace Common
{
    public class Credentials
    {
        public ServicesToken GetToken { get; set; }
        public class ServicesToken
        {
            public string IdentificadorEmpresa { get; set; } = Parametros.GetInstance().IdentificadorEmpresa;
            public string UsuarioEmpresa { get; set; } = Parametros.GetInstance().UsuarioEmpresa;
            public string AutorizacionEmpresa { get; set; } = Parametros.GetInstance().AutorizacionEmpresa;
        }

        public class ResponseServicesToken
        {
            public string Token { get; set; }
        }
    }
}
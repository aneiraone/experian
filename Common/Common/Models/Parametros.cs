using Common.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Parametros
    {
        private readonly string _token = "TOKEN_TIME_MIN";
        private readonly string _urlTokenExperian = "URL_TOKEN_EXPERIAN";
        private readonly string _urlData = "URL_DATA";
        private readonly string _nameExperian = "TOKEN_USER_EXPERIAN";
        private readonly string _passExperian = "TOKEN_PASS_EXPERIAN";
        private readonly string _clientId = "CLIENTID_EXPERIAN";
        private readonly string _clientSecret = "CLIENT_SECRET";
        private readonly string _urlToken = "URL_TOKEN";
        private readonly string _urlCarga = "URL_CARGA";
        private readonly string _autorizacionEmpresa = "AutorizacionEmpresa";
        private readonly string _usuarioEmpresa = "UsuarioEmpresa";
        private readonly string _identificadorEmpresa = "IdentificadorEmpresa";

        private readonly string _emailHost = "EMAIL_SMTP";
        private readonly string _emailName = "EMAIL_FROM";
        private readonly string _emailPass = "EMAIL_PASS";
        private readonly string _emailPort = "EMAIL_PORT";
        private readonly string _emailTo = "EMAIL_TO";

        private readonly string InvalidArgument = " Invalid Arguments Parametros {0}";

        public Parametros() { }
        private static Parametros _instance;
        public static Parametros GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Parametros();
            }
            return _instance;
        }

        public int TimeOut { get; set; }
        public string URLData { get; set; }
        public string URLTokenExperian { get; set; }
        public string TokenNameExperian { get; set; }
        public string TokenPassExperian { get; set; }
        public string ClientIdExperian { get; set; }
        public string ClientSecretExperian { get; set; }
        public string URLCarga { get; set; }
        public string URLTokenApi2020 { get; set; }
        public string IdentificadorEmpresa { get; set; }
        public string UsuarioEmpresa { get; set; }
        public string AutorizacionEmpresa { get; set; }

        public void LoadParametros(List<Parametro> parametros)
        {
            if (!parametros.Exists(x => x.Llave == _token))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _token));
            }

            if (!parametros.Exists(x => x.Llave == _urlToken))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _urlToken));
            }

            if (!parametros.Exists(x => x.Llave == _urlCarga))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _urlCarga));
            }

            if (!parametros.Exists(x => x.Llave == _urlData))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _urlData));
            }

            if (!parametros.Exists(x => x.Llave == _urlTokenExperian))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _urlTokenExperian));
            }

            if (!parametros.Exists(x => x.Llave == _autorizacionEmpresa))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _autorizacionEmpresa));
            }

            if (!parametros.Exists(x => x.Llave == _usuarioEmpresa))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _usuarioEmpresa));
            }

            if (!parametros.Exists(x => x.Llave == _identificadorEmpresa))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _identificadorEmpresa));
            }

            if (!parametros.Exists(x => x.Llave == _nameExperian))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _nameExperian));
            }

            if (!parametros.Exists(x => x.Llave == _passExperian))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _passExperian));
            }

            if (!parametros.Exists(x => x.Llave == _clientId))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _clientId));
            }

            if (!parametros.Exists(x => x.Llave == _clientSecret))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _clientSecret));
            }

            if (!parametros.Exists(x => x.Llave == _emailHost))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _emailHost));
            }
            if (!parametros.Exists(x => x.Llave == _emailName))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _emailName));
            }
            if (!parametros.Exists(x => x.Llave == _emailPass))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _emailPass));
            }
            if (!parametros.Exists(x => x.Llave == _emailPort))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _emailPort));
            }
            if (!parametros.Exists(x => x.Llave == _emailTo))
            {
                throw new ArgumentException(string.Format(InvalidArgument, _emailTo));
            }

            string _timeOut = parametros.Find(x => x.Llave == _token).Valor;
            TimeOut = int.Parse(_timeOut);
            if (TimeOut == 0) { TimeOut = 30; } //DEFAULT VALUE  30 seconds

            URLTokenExperian = parametros.Find(x => x.Llave == _urlTokenExperian).Valor;
            TokenNameExperian = parametros.Find(x => x.Llave == _nameExperian).Valor;
            TokenPassExperian = parametros.Find(x => x.Llave == _passExperian).Valor;
            ClientIdExperian = parametros.Find(x => x.Llave == _clientId).Valor;
            ClientSecretExperian = parametros.Find(x => x.Llave == _clientSecret).Valor;
            URLData = parametros.Find(x => x.Llave == _urlData).Valor;

            URLCarga = parametros.Find(x => x.Llave == _urlCarga).Valor;
            URLTokenApi2020 = parametros.Find(x => x.Llave == _urlToken).Valor;
            IdentificadorEmpresa = parametros.Find(x => x.Llave == _identificadorEmpresa).Valor;
            UsuarioEmpresa = parametros.Find(x => x.Llave == _usuarioEmpresa).Valor;
            AutorizacionEmpresa = parametros.Find(x => x.Llave == _autorizacionEmpresa).Valor;

            string EmailHost = parametros.Find(x => x.Llave == _emailHost).Valor;
            string EmailUser = parametros.Find(x => x.Llave == _emailName).Valor;
            string EmailPass = parametros.Find(x => x.Llave == _emailPass).Valor;
            string _port = parametros.Find(x => x.Llave == _emailPort).Valor;
            int EmailPort = int.Parse(_port);
            if (EmailPort == 0) { EmailPort = 25; }
            List<Parametro> listEmail = parametros.FindAll(x => x.Llave == _emailTo);
            IDictionary<string, string> fields = new Dictionary<string, string>() {
                { _urlData, URLData },
                { _nameExperian, TokenNameExperian},
                { _passExperian ,TokenPassExperian},
                { _clientId, ClientIdExperian},
                { _clientSecret ,ClientSecretExperian},
                { _urlCarga, URLCarga },
                { _urlToken, URLTokenApi2020},
                { _identificadorEmpresa ,IdentificadorEmpresa},
                { _usuarioEmpresa, UsuarioEmpresa},
                { _emailHost ,EmailHost},
                { _emailName ,EmailUser},
                { _emailPass ,EmailPass},
            };

            Validate(fields);
            ValidateEmail(listEmail);

            string EmailTo = string.Join("; ", listEmail.Select(c => c.Valor).ToArray());
            AppSettings.GetInstance().Email.Smtp.Host = EmailHost;
            AppSettings.GetInstance().Email.Smtp.Port = EmailPort;
            AppSettings.GetInstance().Email.Smtp.UserName = EmailUser;
            AppSettings.GetInstance().Email.Smtp.Password = EmailPass;
            AppSettings.GetInstance().Email.Smtp.To = EmailTo;
            AppSettings.GetInstance().Email.Smtp.From = EmailUser;
        }

        private bool Validate(IDictionary<string, string> values)
        {
            bool outresponse = true;
            StringBuilder sb = new StringBuilder("Debe completar los parametros de ");
            foreach (var row in values)
            {
                if (row.Value.Length == 0)
                {
                    sb.Append(string.Format("{0} ", row.Key));
                    outresponse = false;
                }
            }

            if (!outresponse)
            {
                throw new ArgumentException(sb.ToString());
            }
            return outresponse;
        }

        private bool ValidateEmail(List<Parametro> emails)
        {
            EmailAddressAttribute e = new EmailAddressAttribute();
            foreach (var email in emails)
            {
                if (!e.IsValid(email.Valor))
                {
                    throw new FormatException(string.Format("Formato de correo Invalido {0}", email.Valor));
                }
            }
            return true;
        }
    }
}

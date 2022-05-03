using Common;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;

public class Email: IEmail
{
    readonly static string _process_ok = "correctamente";
    readonly static string _process_error = "con errores";
    readonly static string _subject_ok = "Procesado";
    readonly static string _subject_error = "Error";
    public bool Send(ResponseCarga data)
    {
        string body = AppSettings.GetInstance().Email.Template[0].Body;
        string subject = AppSettings.GetInstance().Email.Template[0].Subject;

        if (data.Status == ResponseCarga.statusOK)
        {
            subject = string.Format(subject, _subject_ok, data.Value.Rut, data.Value.TipoDocumento, data.Value.Folio);
            body = string.Format(body, data.Value.Rut, data.Value.TipoDocumento, data.Value.Folio, _process_ok, string.Empty);
        }
        else
        {
            subject = string.Format(subject, _subject_error, data.Value.Rut, data.Value.TipoDocumento, data.Value.Folio);
            body = string.Format(body, data.Value.Rut, data.Value.TipoDocumento, data.Value.Folio, _process_error, TableResult(data.Value.Result));

        }
        return Send(AppSettings.GetInstance().Email.Smtp.From, AppSettings.GetInstance().Email.Smtp.To, subject, body);
    }

    public bool Send(string mensaje)
    {
        string body = AppSettings.GetInstance().Email.Template[1].Body;
        string subject = AppSettings.GetInstance().Email.Template[1].Subject;
        body = string.Format(body, mensaje);
        return Send(AppSettings.GetInstance().Email.Smtp.From, AppSettings.GetInstance().Email.Smtp.To, subject, body);
    }

    private string TableResult(JArray result)
    {
        string table = "<table><thead><tr><th>Detalle</th></tr></thead><tbody>";
        foreach (JObject item in result)
        {
            // item[]
        }


        return table;
    }

    #region "CONFIG EMAIL"
    private bool Send(string from, string _to, string subject, string body) {
        SmtpClient smtpClient = Config;
        smtpClient.Send(from, _to, subject, body);
        return true;
    }

    private SmtpClient Config
    {
        get
        {
            return new SmtpClient(AppSettings.GetInstance().Email.Smtp.Host)
            {
                Port = AppSettings.GetInstance().Email.Smtp.Port,
                Credentials = new NetworkCredential(AppSettings.GetInstance().Email.Smtp.UserName, AppSettings.GetInstance().Email.Smtp.Password),
                //EnableSsl = true,
            };
        }
    }
    #endregion
}
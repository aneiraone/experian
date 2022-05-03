using Common;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

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
        StringBuilder sb = new StringBuilder();
        sb.Append("<table><thead><tr><th>Property</th><th>Message</th></tr></thead>");
        sb.Append("<tbody>");
        foreach (JObject item in result)
        {
            sb.Append("<tr>");
            if (item.ContainsKey("Property"))
            {
                sb.Append(string.Format("<td>{0}</td>", (string)item["Property"]));
            }
            if (item.ContainsKey("Message")) {
                sb.Append(string.Format("<td>{0}</td>", (string)item["Message"]));
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody>");
        return sb.ToString();
    }

    #region "CONFIG EMAIL"
    private bool Send(string from, string _to, string subject, string body) {
        SmtpClient smtpClient = Config;
        MailMessage message = new MailMessage(from, _to);
        message.Subject = subject;
        message.IsBodyHtml = true;
        message.Body = body;
        smtpClient.Send(message);
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
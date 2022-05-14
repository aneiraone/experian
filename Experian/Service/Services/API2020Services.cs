using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

class API2020Services
{
    private readonly string _token = "Token";
    public Token GenerateToken()
    {
        HttpClient client = new HttpClient();
        string stringPayload = JsonConvert.SerializeObject(new Credentials { GetToken = new Credentials.ServicesToken { } });
        StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, Constants.ServiceRest.ContentType);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(Constants.ServiceRest.ContentType);

        client.Timeout = TimeSpan.FromSeconds(Parametros.GetInstance().TimeOut);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(Parametros.GetInstance().URLTokenApi2020, httpContent).Result)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Exception ex = new Exception(response.StatusCode + " " + ((response.StatusCode == HttpStatusCode.NotFound) ?
                    Constants.ExceptionMessage.URLNOVALIDA + Parametros.GetInstance().URLTokenApi2020 : string.Format("{0} - className {1}", response.Content.ReadAsStringAsync().Result, this.GetType().Name)));
                throw ex;
            }

            return SetToken(response.Content.ReadAsStringAsync().Result);
        }
    }

    public string Carga(Token resp, JObject document)
    {
      //  RequestCargaDTE req = new RequestCargaDTE(document);
        string stringPayload = JsonConvert.SerializeObject(document);
        StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, Constants.ServiceRest.ContentType);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(Constants.ServiceRest.ContentType);

        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(Parametros.GetInstance().TimeOut);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resp.token);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(Parametros.GetInstance().URLCarga, httpContent).Result)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return (response.StatusCode == HttpStatusCode.NotFound) ?
                    Constants.ExceptionMessage.URLNOVALIDA + Parametros.GetInstance().URLCarga : response.Content.ReadAsStringAsync().Result;
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }

    private Token SetToken(string ObjResponse)
    {
        dynamic tokenJson = JsonConvert.DeserializeObject(ObjResponse);
        if (!tokenJson.ContainsKey(_token))
        {
            throw new InvalidRequestTokenException(string.Format("{0} - className {1}",_token, this.GetType().Name));
        }
        string token = (string)tokenJson[_token];
        return new Token(token);
    }
}


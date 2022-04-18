using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static Credentials;

class JWTServices
{
    public Token GenerateToken()
    {
        HttpClient client = new HttpClient();
        string stringPayload = JsonConvert.SerializeObject(new Credentials { GetToken = new ServicesToken { } });
        StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, Constants.ServiceRest.ContentType);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(Constants.ServiceRest.ContentType);

        client.Timeout = TimeSpan.FromSeconds(30);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(ConfigurationManager.AppSettings.Get("URL_TOKEN"), httpContent).Result)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Exception ex = new Exception(response.StatusCode + " " + ((response.StatusCode == HttpStatusCode.NotFound) ? 
                    Constants.ExceptionMessage.URLNOVALIDA + ConfigurationManager.AppSettings.Get("URL_TOKEN") : response.Content.ReadAsStringAsync().Result));
                throw ex;
            }

            return SetToken(response.Content.ReadAsStringAsync().Result);
        }
    }

    public string Carga(Token resp, JObject document)
    {
        RequestCargaDTE req = new RequestCargaDTE(document);
        string stringPayload = JsonConvert.SerializeObject(req);
        StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, Constants.ServiceRest.ContentType);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(Constants.ServiceRest.ContentType);

        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(60);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resp.token);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(ConfigurationManager.AppSettings.Get("URL_CARGA"), httpContent).Result)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            { 
                return (response.StatusCode == HttpStatusCode.NotFound) ?
                    Constants.ExceptionMessage.URLNOVALIDA + ConfigurationManager.AppSettings.Get("URL_CARGA") : response.Content.ReadAsStringAsync().Result;
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }

    private Token SetToken(string ObjResponse)
    {
        return new Token(JsonConvert.DeserializeObject<ResponseServicesToken>(ObjResponse).Token);
    }
}


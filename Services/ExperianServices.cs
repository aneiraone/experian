using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static Credentials;

class ExperianServices
{
    public Token GenerateToken()
    {
        HttpClient client = new HttpClient();
        string stringPayload = JsonConvert.SerializeObject(new Credentials { GetToken = new ServicesToken { } });
        StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, Constants.ServiceRest.ContentType);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(Constants.ServiceRest.ContentType);

        client.Timeout = TimeSpan.FromSeconds(30);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(ConfigurationManager.AppSettings.Get("URL_TOKEN_EXPERIAN"), httpContent).Result)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Exception ex = new Exception(response.StatusCode + " " + ((response.StatusCode == HttpStatusCode.NotFound) ? 
                    Constants.ExceptionMessage.URLNOVALIDA + ConfigurationManager.AppSettings.Get("URL_TOKEN_EXPERIAN") : response.Content.ReadAsStringAsync().Result));
                throw ex;
            }

            return SetToken(response.Content.ReadAsStringAsync().Result);
        }
    }

    public string Data()//(Token resp, string json)
    {
        //RequestCargaDTE req = new RequestCargaDTE(json);
        //string stringPayload = JsonConvert.SerializeObject(req);
        StringContent httpContent = new StringContent("", Encoding.UTF8, Constants.ServiceRest.ContentType);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(Constants.ServiceRest.ContentType);

        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(60);
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resp.token);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(ConfigurationManager.AppSettings.Get("URL_DATA"), httpContent).Result)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            { 
                return (response.StatusCode == HttpStatusCode.NotFound) ?
                    Constants.ExceptionMessage.URLNOVALIDA + ConfigurationManager.AppSettings.Get("URL_DATA") : response.Content.ReadAsStringAsync().Result;
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }

    private Token SetToken(string ObjResponse)
    {
        return new Token(JsonConvert.DeserializeObject<ResponseServicesToken>(ObjResponse).Token);
    }
}


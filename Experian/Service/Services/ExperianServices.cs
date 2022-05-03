using Common;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public class ExperianServices
{
    public Token GenerateToken()
    {
        HttpClient client = new HttpClient();
        string stringPayload = JsonConvert.SerializeObject(new Credentials { GetToken = new Credentials.ServicesToken { } });
        StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, Constants.ServiceRest.ContentType);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(Constants.ServiceRest.ContentType);
        
        client.Timeout = TimeSpan.FromSeconds(Parametros.GetInstance().TimeOut);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(Parametros.GetInstance().URLTokenExperian, httpContent).Result)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Exception ex = new Exception(response.StatusCode + " " + ((response.StatusCode == HttpStatusCode.NotFound) ? 
                    Constants.ExceptionMessage.URLNOVALIDA + Parametros.GetInstance().URLTokenExperian : response.Content.ReadAsStringAsync().Result));
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
        client.Timeout = TimeSpan.FromSeconds(Parametros.GetInstance().TimeOut);
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resp.token);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(Parametros.GetInstance().URLData, httpContent).Result)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            { 
                return (response.StatusCode == HttpStatusCode.NotFound) ?
                    Constants.ExceptionMessage.URLNOVALIDA + Parametros.GetInstance().URLData : response.Content.ReadAsStringAsync().Result;
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }

    private Token SetToken(string ObjResponse)
    {
        return new Token(JsonConvert.DeserializeObject<Credentials.ResponseServicesToken>(ObjResponse).Token);
    }
}
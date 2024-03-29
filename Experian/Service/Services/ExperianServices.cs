﻿using Common;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public class ExperianServices
{
    private readonly string _accessToken = "access_token";
    private readonly string _expireTime = "expires_in";
    private readonly string _clientId = "Client_id";
    private readonly string _clientSecret = "Client_secret";
    LogData _logData = LogData.GetInstance();

    public Token GenerateToken()
    {
        HttpClient client = new HttpClient();
        string stringPayload = JsonConvert.SerializeObject(new CredentialsExperian());
        StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, Constants.ServiceRest.ContentType);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(Constants.ServiceRest.ContentType);
        httpContent.Headers.Add(_clientId, Parametros.GetInstance().ClientIdExperian);
        httpContent.Headers.Add(_clientSecret, Parametros.GetInstance().ClientSecretExperian);
        client.Timeout = TimeSpan.FromSeconds(Parametros.GetInstance().TimeOut);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL"));
        using (var response = client.PostAsync(Parametros.GetInstance().URLTokenExperian, httpContent).Result)
        {
            string responseString = response.Content.ReadAsStringAsync().Result;
            _logData.Generar(responseString);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Exception ex = new Exception(response.StatusCode + " " + ((response.StatusCode == HttpStatusCode.NotFound) ?
                    Constants.ExceptionMessage.URLNOVALIDA + Parametros.GetInstance().URLTokenExperian : string.Format("{0} - className {1}", responseString, this.GetType().Name)));
                throw ex;
            }

            return SetToken(responseString);
        }
    }

    public string Data(Token resp)//(, string json)
    {
        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(Parametros.GetInstance().TimeOut);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resp.token);
        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL")); 
        using (var response = client.GetAsync(Parametros.GetInstance().URLData).Result)
        {
            string responseString = response.Content.ReadAsStringAsync().Result;
            _logData.Generar(responseString);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Exception ex = new Exception(response.StatusCode + " " + ((response.StatusCode == HttpStatusCode.NotFound) ?
                   Constants.ExceptionMessage.URLNOVALIDA + Parametros.GetInstance().URLData : responseString));
                throw ex;
            }
            return responseString;
        }
    }

    private Token SetToken(string response)
    {
        dynamic tokenJson = JsonConvert.DeserializeObject(response);
        if (!tokenJson.ContainsKey(_accessToken)) {
            throw new InvalidRequestTokenException(string.Format("{0} - className {1}", _accessToken, this.GetType().Name));
        }
        int expired = 0;
        string token = (string)tokenJson[_accessToken];
        if (tokenJson.ContainsKey(_expireTime))
        {
            expired = (int)tokenJson[_expireTime];
            expired /= 60;
        }
        return new Token(token, expired);
    }
}
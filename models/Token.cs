using System;
using System.Configuration;

public class Token
{
    public Token()
    {
    }

    public Token(string tokenValue)
    {
        token = tokenValue;
        expired = DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings.Get("TOKEN_TIME_MIN")));
    }
    public string token { get; set; }
    public DateTime expired { get; set; }

    public bool ValidateToken(Token token)
    {
        if (token.token == null)
            return false;

        if (DateTime.Now.Subtract(token.expired).TotalSeconds > 0)
            return false;

        return true;
    }
}
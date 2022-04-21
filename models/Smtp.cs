public class Smtp
{
    public string Host { get; set; }
    public int Port { get; set; } = 0;
    public string UserName { get; set; }
    public string Password { get; set; }
    public string From { get; set; }
    public string To { get; set; }

}
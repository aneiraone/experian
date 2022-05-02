using System.Collections.Generic;

namespace Common
{
    public class Smtp
    {
        //public Smtp() { }

        //public Smtp(string host, int port, string userName, string pass, string to, string from) { 
        //    Host = host;
        //    Port = port;
        //    UserName = userName;
        //    Password = pass;
        //    To = to;
        //    From = from;
        //}

        public string Host { get; set; }
        public int Port { get; set; } = 0;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
using System;
using System.IO;
using System.Reflection;

class LogProcess
{
    readonly string ExtensioLog = "_result.log";
    public void Generar(string content)
    {
        string name = DateTime.Now.ToString("yyyy_MM_dd") + ExtensioLog;
        string path = AppDomain.CurrentDomain.BaseDirectory;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using (StreamWriter sw = new StreamWriter(path + @"\" + name, false))
        {
            sw.WriteLine(content);
        }
    }
}


using System;
using System.IO;
using System.Reflection;

class LogProcess
{
    readonly string ExtensioLog = "_result.log";
    readonly string folder = "log";
    public void Generar(string content)
    {
        string name = DateTime.Now.ToString("yyyy_MM_dd") + ExtensioLog;
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using (StreamWriter sw = new StreamWriter(Path.Combine(path,name), false))
        {
            sw.WriteLine(content);
        }
    }
}


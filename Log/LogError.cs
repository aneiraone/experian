using System;
using System.Diagnostics;
using System.IO;

class LogError : ILogError
{
    public static string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "log");
    public void Generar(object obj, Exception ex)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string name = GetNameFile();

            using (StreamWriter sw = new StreamWriter(Path.Combine(path, name), true))
            {
                sw.WriteLine("Fecha: " + DateTime.Now.ToString("dd-MM-yyyy"));
                sw.WriteLine("Hora: " + DateTime.Now.ToString("HH:mm:ss"));
                StackTrace stacktrace = new StackTrace();
                sw.WriteLine("Stack: " + obj.GetType().FullName);
                sw.WriteLine("Error:" + stacktrace.GetFrame(1).GetMethod().Name + " - " + ex.Message);
                sw.WriteLine("");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private string GetNameFile()
    {
        string nombre = "error_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
        return nombre;
    }
}

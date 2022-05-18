using System;
using System.IO;

public class LogData
{
    private static LogData _instance;
    public static LogData GetInstance()
    {
        if (_instance == null)
        {
            _instance = new LogData();
        }
        return _instance;
    }

    string pathData = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "log",
            string.Format("{0}_dataExperian.log", DateTime.Now.ToString("yyyy_MM_dd")));
    public void Generar(string data)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(pathData, true))
            {
                sw.WriteLine(string.Format("{0} [Information] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), data));
                //sw.WriteLine("");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }
    }
}
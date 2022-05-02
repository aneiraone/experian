using Serilog;
using System;
using System.IO;

public class Logger
{
    public Serilog.Core.Logger _Logger;
    public Serilog.Core.Logger _LoggerFile;
    private Logger()
    {
        string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "log",
            string.Format("{0}.log", DateTime.Now.ToString("yyyy_MM_dd")));

        string pathDocuments = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "log",
            string.Format("{0}_documents.log", DateTime.Now.ToString("yyyy_MM_dd")));

        _Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File(path).CreateLogger();
        _LoggerFile = new LoggerConfiguration().WriteTo.File(pathDocuments).CreateLogger();
    }

    private static Logger _instance;

    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

}
namespace IntegracionDocumentos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Serilog.Core.Logger _log = Logger.GetInstance()._Logger;
            Process invoke = new Process();

            _log.Information(Constants.ConsoleMessage.START);
            invoke.Execute();
            _log.Information(Constants.ConsoleMessage.FINISH);
            //Thread.Sleep(3000);
        }
    }
}

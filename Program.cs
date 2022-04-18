using System;
using System.Threading;

namespace IntegracionDocumentos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process invoke = new Process();
            Console.WriteLine(Constants.ConsoleMessage.START);
            invoke.Execute();
            Console.WriteLine(Constants.ConsoleMessage.FINISH);
            //Thread.Sleep(3000);
        }
    }
}

using System;
using System.ServiceModel;

namespace Drg.TestMeteoService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(MeteoService)))
            {
                try
                {
                    serviceHost.Open();
                    Console.WriteLine("WCF-сервис сбора метеоданных запущен. Нажмите <ENTER> для его остановки.");
                    Console.ReadLine();

                    serviceHost.Close();
                }
                catch (TimeoutException timeProblem)
                {
                    Console.WriteLine(timeProblem.Message);
                    Console.ReadLine();
                }
                catch (CommunicationException commProblem)
                {
                    Console.WriteLine(commProblem.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}

using Drg.CommonData;
using System;
using System.ServiceModel;

namespace Drg.TestMeteoService
{
    [ServiceContract]
    public interface IMeteoService
    {
        [OperationContract]
        void FixMeteoData(MeteoData meteoData);
    }

    public class MeteoService : IMeteoService
    {
        public void FixMeteoData(MeteoData meteoData)
        {
            Console.WriteLine("Метеоданные на {0}:", DateTime.Now);
            Console.WriteLine("\tДавление (Па):             {0}", meteoData.Barometer);
            Console.WriteLine("\tТемпература (град. С):     {0}", meteoData.Temperature);
            Console.WriteLine("\tСкорость ветра (м/сек):    {0}", meteoData.WindSpeed);
            Console.WriteLine("\tНаправление ветра (град.): {0}", meteoData.WindDirection);
            Console.WriteLine("\tВлажность (%):             {0}", meteoData.Humidity);
            Console.WriteLine();
        }
    }
}

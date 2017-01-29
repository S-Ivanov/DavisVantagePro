using Drg.CommonData;
using Drg.TestMeteoDriver.MeteoServiceReference;
using System;
using System.Timers;

namespace Drg.TestMeteoDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MeteoServiceClient meteoServiceClient = new MeteoServiceClient())
            {
                Timer timer = new Timer(1000);
                timer.Elapsed += (sender, e) => HandleTimer(meteoServiceClient);
                timer.Start();

                Console.WriteLine("Драйвер сбора метеоданных запущен. Нажмите <ENTER> для его остановки.");
                Console.ReadLine();
            }
        }

        private static void HandleTimer(MeteoServiceClient meteoServiceClient)
        {
            MeteoData meteoData = GetMeteoData();
            meteoServiceClient.FixMeteoData(meteoData);

            Console.WriteLine("Метеоданные на {0}:", DateTime.Now);
            Console.WriteLine("\tДавление (Па):             {0}", meteoData.Barometer);
            Console.WriteLine("\tТемпература (град. С):     {0}", meteoData.Temperature);
            Console.WriteLine("\tСкорость ветра (м/сек):    {0}", meteoData.WindSpeed);
            Console.WriteLine("\tНаправление ветра (град.): {0}", meteoData.WindDirection);
            Console.WriteLine("\tВлажность (%):             {0}", meteoData.Humidity);
            Console.WriteLine();
        }

        static Random random = new Random();

        private static MeteoData GetMeteoData()
        {
            return new MeteoData
            {
                // 763 мм.рт.ст. = 101724.686 Па
                Barometer = NextRandom(random, 101724.686, 500),
                Humidity = (byte)NextRandom(random, 50, 5),
                Temperature = NextRandom(random, 25, 1.5),
                WindDirection = (ushort)NextRandom(random, 90, 5),
                WindSpeed = NextRandom(random, 5, 2)
            };
        }

        static double NextRandom(Random random, double value, double delta)
        {
            double x = random.NextDouble();
            return value + 2 * delta * (x < 0.5 ? -x : x);
        }
    }
}

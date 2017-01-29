using Drg.CommonData;
using System;
using System.Linq;

namespace Drg.DavisVantagePro.Common
{
    /// <summary>
    /// Особеннсти метеостанции Davis Vantage Pro (Pro2)
    /// </summary>
    public static class DavisVantage
    {
        /// <summary>
        /// Размер пакета данных, байт
        /// </summary>
        public const int LOOP_PACKET_SIZE = 99;

        /// <summary>
        /// Заголовок пакета данных
        /// </summary>
        const string LOOP_PACKET_HEADER = "LOO";

        /// <summary>
        /// Смещение значения давления
        /// </summary>
        const int BAROMETER_OFFSET = 7;

        /// <summary>
        /// Минимальное давление
        /// </summary>
        public const double BAROMETER_MIN = 67537;

        /// <summary>
        /// Максимальное давление
        /// </summary>
        public const double BAROMETER_MAX = 109747.625;

        /// <summary>
        /// Смещение значения температуры
        /// </summary>
        const int TEMPERATURE_OFFSET = 12;

        /// <summary>
        /// Минимальная температура
        /// </summary>
        public const int TEMPERATURE_MIN = -40;

        /// <summary>
        /// Максимальная температура
        /// </summary>
        public const int TEMPERATURE_MAX = 60;

        /// <summary>
        /// Смещение значения скорости ветра
        /// </summary>
        const int WINDSPEED_OFFSET = 14;

        /// <summary>
        /// Минимальная скорость ветра
        /// </summary>
        public const double WINDSPEED_MIN = 1.5;

        /// <summary>
        /// Максимальная скорость ветра
        /// </summary>
        public const double WINDSPEED_MAX = 79;

        /// <summary>
        /// Смещение значения направления ветра
        /// </summary>
        const int WINDDIRECTION_OFFSET = 16;

        /// <summary>
        /// Минимальное значение направления ветра
        /// </summary>
        public const int WINDDIRECTION_MIN = 0;

        /// <summary>
        /// Максимальное значение направления ветра
        /// </summary>
        public const int WINDDIRECTION_MAX = 360;

        /// <summary>
        /// Смещение значения влажности
        /// </summary>
        const int HUMIDITY_OFFSET = 33;

        /// <summary>
        /// Минимальная влажность
        /// </summary>
        public const int HUMIDITY_MIN = 0;

        /// <summary>
        /// Максимальная влажность
        /// </summary>
        public const int HUMIDITY_MAX = 100;

        /// <summary>
        /// Смещение значения CRC
        /// </summary>
        const int CRC_OFFSET = 97;

        /// <summary>
        /// Создание данных, получаемых командой LOOP, из массива байтов
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static MeteoData CreateLoopData(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException();

            // проверить размер пакета
            if (bytes.Length != LOOP_PACKET_SIZE)
                throw new ArgumentException("Size");

            // проверить заголовок пакета
            if (!Enumerable.SequenceEqual(bytes.Take(LOOP_PACKET_HEADER.Length), LOOP_PACKET_HEADER.Select(c => (byte)c)))
                throw new ArgumentException("Header");

            // проверить CRC
            ushort crc = BitConverter.ToUInt16(bytes, CRC_OFFSET);
            if (crc != Tools.Crc16Ccitt(bytes, 0, CRC_OFFSET))
                throw new ArgumentException("CRC");

            // формировать результат
            MeteoData loopData = new MeteoData
            {
                Barometer = Tools.HgToPa(BitConverter.ToUInt16(bytes, BAROMETER_OFFSET) / 1000.0),
                Humidity = bytes[HUMIDITY_OFFSET],
                Temperature = Tools.FahrenheitToCelcius(BitConverter.ToUInt16(bytes, TEMPERATURE_OFFSET) / 10.0),
                WindDirection = BitConverter.ToUInt16(bytes, WINDDIRECTION_OFFSET),
                WindSpeed = Tools.MphToMetersPerSec(bytes[WINDSPEED_OFFSET])
            };

            // проверить результат
            CheckLoopData(loopData);

            return loopData;
        }

        /// <summary>
        /// Генерировать пакет данных
        /// </summary>
        /// <param name="loopData"></param>
        /// <returns></returns>
        public static byte[] GenerateLoopDataBytes(MeteoData loopData)
        {
            CheckLoopData(loopData);

            byte[] result = new byte[LOOP_PACKET_SIZE];
            Array.Copy(LOOP_PACKET_HEADER.Select(c => (byte)c).ToArray(), result, LOOP_PACKET_HEADER.Length);
            Array.Copy(BitConverter.GetBytes((ushort)(Tools.PaToHg(loopData.Barometer) * 1000)), 0, result, BAROMETER_OFFSET, 2);
            Array.Copy(BitConverter.GetBytes((ushort)(Tools.CelciusToFahrenheit(loopData.Temperature) * 10)), 0, result, TEMPERATURE_OFFSET, 2);
            result[WINDSPEED_OFFSET] = (byte)Tools.MetersPerSecToMph(loopData.WindSpeed);
            Array.Copy(BitConverter.GetBytes(loopData.WindDirection), 0, result, WINDDIRECTION_OFFSET, 2);
            result[HUMIDITY_OFFSET] = loopData.Humidity;
            Array.Copy(BitConverter.GetBytes(Tools.Crc16Ccitt(result, 0, CRC_OFFSET)), 0, result, CRC_OFFSET, 2);
            return result;
        }

        /// <summary>
        /// Проверка данных
        /// </summary>
        /// <param name="loopData"></param>
        public static void CheckLoopData(MeteoData loopData)
        {
            if (loopData == null)
                throw new ArgumentNullException();

            if (!Tools.InRange(loopData.Barometer, BAROMETER_MIN, BAROMETER_MAX))
                throw new ArgumentException("Barometer");

            if (!Tools.InRange(loopData.Temperature, TEMPERATURE_MIN, TEMPERATURE_MAX))
                throw new ArgumentException("Temperature");

            if (!Tools.InRange(loopData.WindSpeed, WINDSPEED_MIN, WINDSPEED_MAX))
                throw new ArgumentException("WindSpeed");

            if (!Tools.InRange(loopData.WindDirection, WINDDIRECTION_MIN, WINDDIRECTION_MAX))
                throw new ArgumentException("WindDirection");

            if (!Tools.InRange(loopData.Humidity, HUMIDITY_MIN, HUMIDITY_MAX))
                throw new ArgumentException("Humidity");
        }
    }
}

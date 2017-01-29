using System.Runtime.Serialization;

namespace Drg.CommonData
{
    /// <summary>
    /// Метеоданные
    /// </summary>
    [DataContract]
    public class MeteoData
    {
        /// <summary>
        /// Атмосферное давление, Па
        /// </summary>
        [DataMember]
        public double Barometer { get; set; }

        /// <summary>
        /// Температура, град. С
        /// </summary>
        [DataMember]
        public double Temperature { get; set; }

        /// <summary>
        /// Скорость ветра, м/сек
        /// </summary>
        [DataMember]
        public double WindSpeed { get; set; }

        /// <summary>
        /// Направление ветра, град. (1..360)
        /// </summary>
        /// <remarks>
        /// 0 - нет данных
        /// 90 - восток
        /// 180 - юг
        /// 270 - запад
        /// 360 - север
        /// </remarks>
        [DataMember]
        public ushort WindDirection { get; set; }

        /// <summary>
        /// Влажность, %
        /// </summary>
        [DataMember]
        public byte Humidity { get; set; }

        // Outside Alarms
    }
}

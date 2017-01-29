using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Drg.DavisVantagePro.Common;
using Drg.CommonData;

namespace UnitTests
{
    [TestClass]
    public class DavisVantage_Tests
    {
        [TestMethod]
        public void DavisVantage_CreateLoopData()
        {
            MeteoData loopData = new MeteoData
            {
                // 763 мм.рт.ст. = 101724.686 Па
                Barometer = 101724.686,
                Humidity = 50,
                Temperature = 25,
                WindDirection = 90,
                WindSpeed = 3.5
            };

            MeteoData createdLoopData = DavisVantage.CreateLoopData(DavisVantage.GenerateLoopDataBytes(loopData));

            Assert.AreEqual(loopData.Barometer, createdLoopData.Barometer, 1.0);
            Assert.AreEqual(loopData.Humidity, createdLoopData.Humidity);
            Assert.AreEqual(loopData.Temperature, createdLoopData.Temperature, 0.1);
            Assert.AreEqual(loopData.WindDirection, createdLoopData.WindDirection);
            Assert.AreEqual(loopData.WindSpeed, createdLoopData.WindSpeed, 0.5);
        }
    }
}

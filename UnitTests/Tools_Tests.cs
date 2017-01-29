using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Drg.DavisVantagePro.Common;

namespace UnitTests
{
    [TestClass]
    public class Tools_Tests
    {
        /// <summary>
        /// Для тестирования использован online-калькулятор (<see cref="http://depa.usst.edu.cn/chenjq/www2/software/crc/CRC_Javascript/CRCcalculation.htm"/>)
        /// </summary>
        [TestMethod]
        public void Tools_Crc16Ccitt()
        {
            Assert.AreEqual(0x9dd6, Tools.Crc16Ccitt(new byte[] { 0x61, 0x62, 0x63 }));
        }

        /// <summary>
        /// Для тестирования использован online-калькулятор (<see cref="http://www.edinici.ru/Perevodchik-Skorosti/Kalkulyator-Skorosti.aspx"/>)
        /// </summary>
        [TestMethod]
        public void Tools_MphToMetersPerSec()
        {
            Assert.AreEqual(4.4704, Tools.MphToMetersPerSec(10), 0.0001);
        }

        /// <summary>
        /// Для тестирования использован online-калькулятор (<see cref="http://www.edinici.ru/Perevodchik-Skorosti/Kalkulyator-Skorosti.aspx"/>)
        /// </summary>
        [TestMethod]
        public void Tools_MetersPerSecToMph()
        {
            Assert.AreEqual(11.1847, Tools.MetersPerSecToMph(5), 0.0001);
        }

        /// <summary>
        /// Для тестирования использован online-калькулятор (<see cref="http://www.edinici.ru/Perevodchik-Temperaturi/Kalkulyator-Temperaturi.aspx"/>)
        /// </summary>
        [TestMethod]
        public void Tools_FahrenheitToCelcius()
        {
            Assert.AreEqual(18.3333, Tools.FahrenheitToCelcius(65), 0.0001);
        }

        /// <summary>
        /// Для тестирования использован online-калькулятор (<see cref="http://www.edinici.ru/Perevodchik-Temperaturi/Kalkulyator-Temperaturi.aspx"/>)
        /// </summary>
        [TestMethod]
        public void Tools_CelciusToFahrenheit()
        {
            Assert.AreEqual(77, Tools.CelciusToFahrenheit(25), 0.0001);
        }

        /// <summary>
        /// Для тестирования использован online-калькулятор (<see cref="https://www.translatorscafe.com/unit-converter/ru/pressure/"/>)
        /// </summary>
        [TestMethod]
        public void Tools_HgToPa()
        {
            Assert.AreEqual(108.0592, Tools.HgToPa(0.032), 0.0001);
        }

        /// <summary>
        /// Для тестирования использован online-калькулятор (<see cref="https://www.translatorscafe.com/unit-converter/ru/pressure/"/>)
        /// </summary>
        [TestMethod]
        public void Tools_PaToHg()
        {
            Assert.AreEqual(0.2961, Tools.PaToHg(1000), 0.0001);
        }
    }
}

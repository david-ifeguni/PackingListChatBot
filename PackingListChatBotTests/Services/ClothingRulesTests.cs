using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackingListChatBot.Models;
using PackingListChatBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingListChatBot.Services.Tests
{
    [TestClass()]
    public class ClothingRulesTests
    {
        [TestMethod()]
        public void EvaluateTest_ShouldRequireWarmLayers()
        {
            WeatherProfile mockWeatherProfile = new WeatherProfile()
            {
                AverageLowF = 40.0
            };

            var clothingRules = new ClothingRules();

            var result = clothingRules.Evaluate(mockWeatherProfile);

            Assert.IsTrue(result.NeedsWarmLayers);
        }

        [TestMethod()]
        public void EvaluateTest_NeedsRainProtectionAndBreathableClothing()
        {
            WeatherProfile mockWeatherProfile = new WeatherProfile()
            {
                NumOfRainDays = 10,
                AverageHighF = 70,
                HumidityPercentage = 80

            };

            var clothingRules = new ClothingRules();

            var result = clothingRules.Evaluate(mockWeatherProfile);

            Assert.IsTrue(result.NeedsRainProtection);
            Assert.IsTrue(result.NeedsBreathableClothing);
        }

        [TestMethod()]
        public void EvaluateTest_NeedsSunProtectionAndHasLargeTempSwings()
        {
            WeatherProfile mockWeatherProfile = new WeatherProfile()
            {
                ElevationLevelMeters = 1600,
                AverageHighF = 70,
                AverageLowF = 50

            };

            var clothingRules = new ClothingRules();

            var result = clothingRules.Evaluate(mockWeatherProfile);

            Assert.IsTrue(result.NeedsSunProtection);
            Assert.IsTrue(result.LargeTempSwing);
        }
    }
}
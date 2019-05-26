using System;
using System.Collections.Generic;
using System.Configuration;
using BusinessInsightsAPI.Exceptions;
using BusinessInsightsAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessInsightsTest
{
    [TestClass]
    public class FizzBuzzTest
    {
        private string limit = string.Empty;

        [TestInitialize]
        public void TestInitialize()
        {
            limit = ConfigurationManager.AppSettings["limit"];
        }

        [TestMethod]
        public void NumberLowerThanOne_Test()
        {
            List<string> seriesTest = FizzBuzz.DoFizzBuzzSeries(0);
            Assert.AreEqual(seriesTest[0], "The number must be greater than 0 or lower than " + limit);
        }

        [TestMethod]
        public void NumberGreaterThanLimit_Test()
        {
            List<string> seriesTest = FizzBuzz.DoFizzBuzzSeries(5000);
            Assert.AreEqual(seriesTest[0], "The number must be greater than 0 or lower than " + limit);
        }

        [TestMethod]
        public void NumberIsFizz_Test()
        {
            List<string> seriesTest = FizzBuzz.DoFizzBuzzSeries(69);
            Assert.AreEqual(seriesTest[0], "Fizz");
        }

        [TestMethod]
        public void NumberIsBuzz_Test()
        {
            List<string> seriesTest = FizzBuzz.DoFizzBuzzSeries(70);
            Assert.AreEqual(seriesTest[0], "Buzz");
        }

        [TestMethod]
        public void NumberIsFizzBuzz_Test()
        {
            List<string> seriesTest = FizzBuzz.DoFizzBuzzSeries(15);
            Assert.AreEqual(seriesTest[0], "FizzBuzz");
        }

        [TestMethod]
        public void NumberIsNotDivibleByFizzOrBuzz_Test()
        {
            List<string> seriesTest = FizzBuzz.DoFizzBuzzSeries(17);
            Assert.AreEqual(seriesTest[0], "17");
        }

        [TestMethod]
        public void FizzBuzzSeriesHasCorrectSize_Test()
        {
            List<string> seriesTest = FizzBuzz.DoFizzBuzzSeries(50);
            Assert.AreEqual(seriesTest.Count, 26);
        }

    }
}

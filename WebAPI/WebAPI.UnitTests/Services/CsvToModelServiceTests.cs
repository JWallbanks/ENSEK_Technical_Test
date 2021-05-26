using NUnit.Framework;
using System;
using WebAPI.Services;

namespace WebAPI.UnitTests.Services
{
    [TestFixture]
    public class CsvToModelServiceTests
    {
        private CsvToModelService _service;

        [SetUp]
        public void Setup()
        {
            _service = new CsvToModelService();
        }



        [Test]
        public void Method_Scenario_ExpectedResult()
        {
            throw new NotImplementedException();
        }
    }
}

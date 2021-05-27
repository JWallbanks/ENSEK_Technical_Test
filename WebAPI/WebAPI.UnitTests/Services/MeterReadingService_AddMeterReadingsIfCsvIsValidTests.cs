using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.UnitTests.Services
{
    [TestFixture]
    public class MeterReadingService_AddMeterReadingsIfCsvIsValidTests
    {
        private Mock<IUnitOfWork> _uow;
        private MeterReadingControllerService _meterReadingService;

        private string validCsvLine = "1,01/01/2000 00:00,11111";
        private int validAccountId = 1;
        private int validMeterReadingValue = 11111;
        private DateTime validMeterReadingDate = new DateTime(2000, 1, 1);

        private string csvLineWithHeaders = "AccountId,MeterReadingDate,MeterReadValue";


        [SetUp]
        public void Setup()
        {
            _uow = new Mock<IUnitOfWork>();

            // Have the setups by default work as if the csv values are valid for the existing database
            _uow.Setup(u => u.AccountRepositoryAsync.FindAsync(validAccountId))
                .ReturnsAsync(new Account { AccountId = validAccountId });
            _uow.Setup(u => u.MeterReadingRepositoryAsync.DoesMeterReadingExistInDb(validAccountId, validMeterReadingDate))
                .Returns(false);
            _uow.Setup(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()))
                .ReturnsAsync(() => new MeterReading());


            _meterReadingService = new MeterReadingControllerService(_uow.Object);
        }


        [Test]
        public async Task AddMeterReadingsIfCsvIsValid_CsvContainsValidMeterReading_AddsTheValidMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                csvLineWithHeaders,
                validCsvLine
            };

            await _meterReadingService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync
            .InsertAsync(It.Is<MeterReading>(m => m.Account.AccountId == validAccountId && m.MeterReadValue == validMeterReadingValue)));
        }

        [Test]
        public async Task AddMeterReadingsIfCsvIsValid_WhenCalled_ReturnsTheNumberOfSuccessfulReadings()
        {
            var csvLines = new List<string>
            {
                csvLineWithHeaders,
                validCsvLine,
                "a,a,a",
                "b,b,b",
            };

            var result = await _meterReadingService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task AddMeterReadingsIfCsvIsValid_MeterReadingHasSameAccountIdAndDateAsExistingReadingInDb_DoesNotAddMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                csvLineWithHeaders,
                validCsvLine,
            };
            _uow.Setup(u => u.MeterReadingRepositoryAsync.DoesMeterReadingExistInDb(validAccountId, validMeterReadingDate))
                .Returns(true);

            await _meterReadingService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()), Times.Never);
        }

        [Test]
        public async Task AddMeterReadingsIfCsvIsValid_MeterReadingAccountIdDoesNotExistInDb_DoesNotAddMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                csvLineWithHeaders,
                validCsvLine,
            };
            _uow.Setup(u => u.AccountRepositoryAsync.FindAsync(validAccountId))
                .ReturnsAsync(() => null);

            await _meterReadingService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()), Times.Never);
        }


        [Test]
        [TestCase("1111")]
        [TestCase("111")]
        [TestCase("11")]
        [TestCase("1")]
        public async Task AddMeterReadingsIfCsvIsValid_MeterReadingValueNotFiveDigits_DoesNotAddMeterReadingToDb(string meterReadingValue)
        {
            var csvLines = new List<string>
            {
                csvLineWithHeaders,
                "1,01/01/2000 00:00," + meterReadingValue,
            };

            await _meterReadingService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()), Times.Never);
        }

    }
}

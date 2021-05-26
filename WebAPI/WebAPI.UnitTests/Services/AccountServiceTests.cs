using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.UnitTests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IUnitOfWork> _uow;
        private AccountService _accountService;

        [SetUp]
        public void Setup()
        {
            _uow = new Mock<IUnitOfWork>();
            _accountService = new AccountService(_uow.Object);
        }

        // Scenarios:
        // 1) some CsvLines are invalid - those CsvLines aren't added to the database
        // 2) some CsvLines are invalid - the number of successful valid lines should be returned as the no. of successful readings
        // 3) invalid if meter reading has the same 'AccountId' & 'Date' as an already existing one.
        // 4) invalid if meter reading doesn't match an existing 'AccountId'
        // 5) invalid if reading value isn't in format 'NNNNN'

        [Test]
        public void AddMeterReadingsIfCsvIsValid_CsvContainsValidMeterReading_AddsTheValidMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00,11111"
            };

            _accountService.AddMeterReadingsIfCsvIsValid(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync
            .InsertAsync(It.Is<MeterReading>(m => m.Account.AccountId == 1 && m.MeterReadValue == 11111)));
        }

        [Test]
        public void AddMeterReadingsIfCsvIsValid_WhenCalled_ReturnsTheNumberOfSuccessfulReadings()
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00,11111",
                "a,a,a",
                "b,b,b",
            };

            var result = _accountService.AddMeterReadingsIfCsvIsValid(csvLines);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddMeterReadingsIfCsvIsValid_MeterReadingHasSameAccountIdAndDateAsExistingReadingInDb_DoesNotAddMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00,11111",
            };
            _uow.Setup(u => u.MeterReadingRepositoryAsync.DoesMeterReadingExistInDb(1, new DateTime(2000, 1, 1)))
                .Returns(true);

            _accountService.AddMeterReadingsIfCsvIsValid(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()), Times.Never);
        }

        [Test]
        public void AddMeterReadingsIfCsvIsValid_MeterReadingAccountIdDoesNotExistInDb_DoesNotAddMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00,11111",
            };
            _uow.Setup(u => u.AccountRepositoryAsync.FindAsync(1))
                .ReturnsAsync(() => null);

            _accountService.AddMeterReadingsIfCsvIsValid(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()), Times.Never);
        }


        [Test]
        [TestCase("1111")]
        [TestCase("111")]
        [TestCase("11")]
        [TestCase("1")]
        public void AddMeterReadingsIfCsvIsValid_MeterReadingValueNotFiveDigits_DoesNotAddMeterReadingToDb(string meterReadingValue)
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00," + meterReadingValue,
            };

            _accountService.AddMeterReadingsIfCsvIsValid(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()), Times.Never);
        }

    }
}

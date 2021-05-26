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
    public class AccountServiceTests
    {
        private Mock<IUnitOfWork> _uow;
        private AccountService _accountService;

        [SetUp]
        public void Setup()
        {
            _uow = new Mock<IUnitOfWork>();

            // Have the setups by default work as if the csv values are valid for the existing database
            _uow.Setup(u => u.AccountRepositoryAsync.FindAsync(1))
                .ReturnsAsync(new Account { AccountId = 1 });
            _uow.Setup(u => u.MeterReadingRepositoryAsync.DoesMeterReadingExistInDb(1, new DateTime(2000, 1, 1)))
                .Returns(false);
            _uow.Setup(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()))
                .ReturnsAsync(() => new MeterReading());


            _accountService = new AccountService(_uow.Object);
        }


        [Test]
        public async Task AddMeterReadingsIfCsvIsValid_CsvContainsValidMeterReading_AddsTheValidMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00,11111"
            };

            await _accountService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync
            .InsertAsync(It.Is<MeterReading>(m => m.Account.AccountId == 1 && m.MeterReadValue == 11111)));
        }

        [Test]
        public async Task AddMeterReadingsIfCsvIsValid_WhenCalled_ReturnsTheNumberOfSuccessfulReadings()
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00,11111",
                "a,a,a",
                "b,b,b",
            };

            var result = await _accountService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task AddMeterReadingsIfCsvIsValid_MeterReadingHasSameAccountIdAndDateAsExistingReadingInDb_DoesNotAddMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00,11111",
            };
            _uow.Setup(u => u.MeterReadingRepositoryAsync.DoesMeterReadingExistInDb(1, new DateTime(2000, 1, 1)))
                .Returns(true);

            await _accountService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()), Times.Never);
        }

        [Test]
        public async Task AddMeterReadingsIfCsvIsValid_MeterReadingAccountIdDoesNotExistInDb_DoesNotAddMeterReadingToDb()
        {
            var csvLines = new List<string>
            {
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00,11111",
            };
            _uow.Setup(u => u.AccountRepositoryAsync.FindAsync(1))
                .ReturnsAsync(() => null);

            await _accountService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

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
                "AccountId,MeterReadingDate,MeterReadValue",
                "1,01/01/2000 00:00," + meterReadingValue,
            };

            await _accountService.AddMeterReadingsToDbIfCsvIsValidAsync(csvLines);

            _uow.Verify(u => u.MeterReadingRepositoryAsync.InsertAsync(It.IsAny<MeterReading>()), Times.Never);
        }

    }
}

using Moq;
using NUnit.Framework;
using System;
using WebAPI.DAL;
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
        public void Method_Scenario_ExpectedResult()
        {
            throw new NotImplementedException();
        }
    }
}

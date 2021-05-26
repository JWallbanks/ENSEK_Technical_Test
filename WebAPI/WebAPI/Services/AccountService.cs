using System.Collections.Generic;
using WebAPI.DAL;

namespace WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public int AddMeterReadingsIfCsvIsValid(IEnumerable<string> csvFileLines)
        {
            return 0;
        }
    }
}

using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICsvToModelService
    {
        IEnumerable<Account> CreateAccountsFromCsvFile(string fileName);
    }
}
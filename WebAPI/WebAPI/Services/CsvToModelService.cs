using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class CsvToModelService : ICsvToModelService
    {
        public IEnumerable<Account> CreateAccountsFromCsvFile(string filepath)
        {
            filepath = Directory.GetCurrentDirectory() + filepath;
            var accounts = File.ReadAllLines(filepath)
                .Skip(1)
                .Select(line => CreateAccountFromCsvLine(line))
                .ToList();

            return accounts;
        }

        private Account CreateAccountFromCsvLine(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Account account = new Account();
            account.AccountId = Convert.ToInt32(values[0]);
            account.FirstName = values[1];
            account.LastName = values[2];

            return account;
        }

    }
}

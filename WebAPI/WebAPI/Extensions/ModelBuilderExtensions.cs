using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder, ICsvToModelService csvToModelService)
        {
            SeedAccounts(modelBuilder, csvToModelService);
        }

        private static void SeedAccounts(ModelBuilder modelBuilder, ICsvToModelService csvToModelService)
        {
            var accountsCsvFilepath = @"\Files\Test_Accounts.csv";
            IEnumerable<Account> list = csvToModelService.CreateAccountsFromCsvFile(accountsCsvFilepath);

            modelBuilder.Entity<Account>().HasData(list);
        }


    }
}

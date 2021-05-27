using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IAccountRepositoryAsync : IRepositoryAsync<Account>
    {
        Task<Account> GetAccountWithMeterReadings(int accountId);
    }
}
using System;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<Account> AccountRepositoryAsync { get; }
        IMeterReadingRepositoryAsync MeterReadingRepositoryAsync { get; }

        Task CommitAsync();
    }

}
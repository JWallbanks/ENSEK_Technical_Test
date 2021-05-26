using System;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<Account> AccountRepositoryAsync { get; }
        IRepositoryAsync<MeterReading> MeterReadingRepositoryAsync { get; }

        Task CommitAsync();
    }

}
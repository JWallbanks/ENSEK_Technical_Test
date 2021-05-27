using System;
using System.Threading.Tasks;

namespace WebAPI.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepositoryAsync AccountRepositoryAsync { get; }
        IMeterReadingRepositoryAsync MeterReadingRepositoryAsync { get; }

        Task CommitAsync();
    }

}
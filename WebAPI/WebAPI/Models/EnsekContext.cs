using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;
using WebAPI.Services;

namespace WebAPI.Models
{
    public class EnsekContext : DbContext
    {
        private readonly ICsvToModelService _csvToModelService;

        public EnsekContext(DbContextOptions<EnsekContext> options, ICsvToModelService csvToModelService) : base(options)
        {
            _csvToModelService = csvToModelService;
        }

        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed(_csvToModelService);

        }

    }
}

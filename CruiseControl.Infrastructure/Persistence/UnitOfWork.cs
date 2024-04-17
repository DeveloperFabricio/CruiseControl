using Microsoft.Extensions.Logging;

namespace CruiseControl.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(AppDbContext appDbContext, ILogger<UnitOfWork> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _appDbContext.Set<TEntity>().Remove(entity);
            return await SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task<bool> Sucesso()
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes to database.");
                return false;
            }
        }

        public async Task<string> MensagemErro()
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

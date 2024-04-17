using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseControl.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        Task<bool> Sucesso();
        Task<string> MensagemErro();
        Task<int> SaveChangesAsync();
        Task<int> CommitAsync();
        Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}

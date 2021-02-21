using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentEngine.Infrastructure.Repositories.Interfaces
{
    public interface IUnitofWork
    {
        Task<bool> Complete(CancellationToken cancellationToken = default(CancellationToken));
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}

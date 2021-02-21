
using Microsoft.EntityFrameworkCore;
using PaymentEngine.Infrastructure.DataAccess;
using PaymentEngine.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentEngine.Infrastructure.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        private Hashtable _repositories;
        private readonly PaymentEngineContext _context;

        public UnitofWork(PaymentEngineContext context)
        {

            _context = context;
        }
        public async Task<bool> Complete(CancellationToken cancellationToken = default(CancellationToken))
        {

            return await _context.SaveChangesAsync() > 0;
        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IRepository<TEntity>)_repositories[type];
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}


using Core.Common.BaseClasses;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork
{
    public sealed class ReadDbUnitOfWork : IReadDbUnitOfWork
    {
        private readonly ReadDbContext _readContext;
        private IDbContextTransaction _transaction;
        private readonly IMediator _mediator;
        private readonly Dictionary<Type, object> _repositories = new();

        public ReadDbUnitOfWork(ReadDbContext readContext, WriteDbContext writeContext)
        {
            _readContext = readContext;
        }


        public IReadRepository<T> ReadRepository<T>() where T : BaseEntity
        {
            if (_repositories.TryGetValue(typeof(T), out var repo))
                return (IReadRepository<T>)repo;

            var newRepo = new ReadRepository<T>(_readContext);
            _repositories.Add(typeof(T), newRepo);
            return newRepo;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already started");

            _transaction = await _readContext.Database.BeginTransactionAsync(cancellationToken);
            return _transaction;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not started");


            await _readContext.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not started");

            await _transaction.RollbackAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _readContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

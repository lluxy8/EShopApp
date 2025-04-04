﻿using Core.Common.BaseClasses;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork
{
    public sealed class WriteDbUnitOFWork : IWriteDbUnitOfWork
    {
        private readonly WriteDbContext _writeContext;
        private IDbContextTransaction _transaction;
        private readonly IMediator _mediator;
        private readonly Dictionary<Type, object> _repositories = new();

        public WriteDbUnitOFWork(ReadDbContext readContext, WriteDbContext writeContext)
        {
            _writeContext = writeContext;
        }

        public IWriteRepository<T> WriteRepository<T>() where T : BaseEntity
        {
            if (_repositories.TryGetValue(typeof(T), out var repo))
                return (IWriteRepository<T>)repo;

            var newRepo = new WriteRepository<T>(_writeContext);
            _repositories.Add(typeof(T), newRepo);
            return newRepo;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already started");

            _transaction = await _writeContext.Database.BeginTransactionAsync(cancellationToken);
            return _transaction;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not started");


            await _writeContext.SaveChangesAsync(cancellationToken);
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
            _writeContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

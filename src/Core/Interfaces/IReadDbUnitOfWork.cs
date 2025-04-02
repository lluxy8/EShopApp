using Core.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReadDbUnitOfWork : IUnitOfWork
    {
        IReadRepository<T> ReadRepository<T>() where T : BaseEntity;
    }
}

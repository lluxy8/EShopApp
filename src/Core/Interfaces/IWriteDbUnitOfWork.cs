using Core.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IWriteDbUnitOfWork : IUnitOfWork
    {
        IWriteRepository<T> WriteRepository<T>() where T : BaseEntity;
    }
}

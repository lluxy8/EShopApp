using Core.Common.Results;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors.RequestPipeline
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IWriteDbUnitOfWork _uow;

        public TransactionBehavior(IWriteDbUnitOfWork uow) => _uow = uow;
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellation)
        {
            await _uow.BeginTransactionAsync(cancellation);

            try
            {
                var response = await next(cancellation);

                if (typeof(TRequest).Name.EndsWith("Command"))
                {
                    if (response is IResult result)
                    {
                                                                  
                        if (result.IsSuccess)
                            await _uow.CommitAsync(cancellation);
                        else
                            await _uow.RollbackAsync(cancellation);
                    }
                }

                return response;
            }
            catch (Exception)
            {
                await _uow.RollbackAsync(cancellation);
                throw;
            }
        }
    }
}

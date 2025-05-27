using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Core.Abstractions;

public interface IQueryValidationHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<Result<TResponse, ErrorList>> Handle(
        TQuery query,
        CancellationToken cancellationToken = default);
}
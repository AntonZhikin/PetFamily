using CSharpFunctionalExtensions;
using PetFamily.Kernel;

namespace PetFamily.Core.Abstractions;

public interface IQueryHandler<TResponce, in TQuery> where TQuery : IQuery
{
    public Task<TResponce> Handle(TQuery query, CancellationToken cancellationToken = default);
}
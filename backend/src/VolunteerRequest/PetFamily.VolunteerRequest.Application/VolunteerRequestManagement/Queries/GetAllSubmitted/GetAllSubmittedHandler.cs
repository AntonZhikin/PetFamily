using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.Volunteer;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.Kernel;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllSubmitted;

public class GetAllSubmittedHandler :
    IQueryValidationHandler<PagedList<VolunteerRequestDto>, 
        GetAllSubmittedQuery>
{
    private readonly IValidator<GetAllSubmittedQuery> _validator;
    private readonly IVolunteerRequestsReadDbContext _readDbContext;

    public GetAllSubmittedHandler(
        IValidator<GetAllSubmittedQuery> validator,
        IVolunteerRequestsReadDbContext readDbContext)
    {
        _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<VolunteerRequestDto>, ErrorList>> Handle(
        GetAllSubmittedQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var requestsQuery = _readDbContext.VolunteerRequests.WhereIf(true,
            x => x.Status == RequestStatus.Submitted.ToString());
        
        return await requestsQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.Volunteer;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.Kernel;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Application.VolunteerRequestManagement.Queries.GetAllByAdminId;

public class GetAllByAdminIdHandler :
    IQueryValidationHandler<PagedList<VolunteerRequestDto>, 
        GetAllByAdminIdQuery>
{
    private readonly IValidator<GetAllByAdminIdQuery> _validator;
    private readonly IVolunteerRequestsReadDbContext _readDbContext;

    public GetAllByAdminIdHandler(
        IValidator<GetAllByAdminIdQuery> validator,
        IVolunteerRequestsReadDbContext readDbContext)
    {
        _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<VolunteerRequestDto>, ErrorList>> Handle(
        GetAllByAdminIdQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var requestsQuery = _readDbContext.VolunteerRequests.WhereIf(true,
            x => x.Status == query.Status).AsQueryable();
        
        requestsQuery = requestsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(
                query.AdminId.ToString()), x => x.AdminId == query.AdminId);
        
        requestsQuery = requestsQuery.WhereIf(
            string.IsNullOrWhiteSpace(
                query.AdminId.ToString()), x => x.Status == RequestStatus.OnReview.ToString());
        
        requestsQuery = requestsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(
                query.AdminId.ToString()), x => x.Status == query.Status);
        
        return await requestsQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
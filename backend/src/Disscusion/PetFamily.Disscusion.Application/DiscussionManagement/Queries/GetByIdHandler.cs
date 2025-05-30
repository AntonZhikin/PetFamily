using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.Discussion;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Queries;

public class GetByIdHandler :
    IQueryValidationHandler<DiscussionDto, GetByIdQuery>
{
    private readonly IValidator<GetByIdQuery> _validator;
    private readonly IDiscussionsReadDbContext _readDbContext;
    private readonly ILogger<GetByIdHandler> _logger;

    public GetByIdHandler(
        IValidator<GetByIdQuery> validator,
        IDiscussionsReadDbContext readDbContext,
        ILogger<GetByIdHandler> logger)
    {
        _validator = validator;
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<DiscussionDto, ErrorList>> Handle(
        GetByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(
            query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussionDto = await _readDbContext.Discussions
            .Include(d => d.Messages)
            .FirstOrDefaultAsync(d => d.Id == query.DiscussionId,
                cancellationToken);

        if (discussionDto is null)
        {
            _logger.LogWarning(
                "Failed to query discussion {discussionId}", query.DiscussionId);
            return Errors.General.NotFound(query.DiscussionId).ToErrorList();
        }

        _logger.LogInformation("Discussion {id} queried", query.DiscussionId);

        return discussionDto;
    }
}
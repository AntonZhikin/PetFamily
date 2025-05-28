using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands.Create;
using PetFamily.Disscusion.Domain.ValueObject;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands;

public class CreateHandler : ICommandHandler<Guid, CreateCommand>
{
    private readonly ILogger<CreateHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCommand> _validator;
    private readonly IDiscussionsRepository _repository;

    public CreateHandler(
        ILogger<CreateHandler> logger,
        [FromKeyedServices(Modules.Disscusion)] IUnitOfWork unitOfWork,
        IValidator<CreateCommand> validator,
        IDiscussionsRepository repository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _repository = repository;
    }
    public async Task<Result<Guid, ErrorList>> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var transaction =  await _unitOfWork.BeginTransaction(cancellationToken);
        
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussionId = Guid.NewGuid();

        var discussionUsers = DiscussionUsers.Create(command.ApplicantUserId, command.ReviewingUserId);
        
        var discussion = Domain.AggregateRoot.Discussion.Create(command.RelationId, discussionUsers);

        if (discussion.IsFailure)
            return discussion.Error.ToErrorList();

        await _repository.Add(discussion.Value, cancellationToken);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        transaction.Commit();

        return discussionId;
    }
}
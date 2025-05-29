using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Disscusion.Domain;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.Close;

public class CloseHandler : ICommandHandler<Guid, CloseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CloseHandler> _logger;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IValidator<CloseCommand> _validator;

    public CloseHandler(
        [FromKeyedServices(Modules.Disscusion)]IUnitOfWork unitOfWork, 
        ILogger<CloseHandler> logger,
        IDiscussionsRepository discussionsRepository,
        IValidator<CloseCommand> validator
        )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _discussionsRepository = discussionsRepository;
        _validator = validator;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(CloseCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(
            command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var discussionExist = await _discussionsRepository
            .GetById(command.DiscussiondId, cancellationToken);
        if (discussionExist.IsFailure)
            return Errors.General.NotFound(command.DiscussiondId).ToErrorList();
        
        if (discussionExist.Value.Status == DiscussionStatus.Closed)
            return Errors.Discussion.AlreadyClosed().ToErrorList();
        
        discussionExist.Value.Close();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Discussion with {Id} closed",
            discussionExist.Value.Id);

        return discussionExist.Value.Id;
        
    }
}
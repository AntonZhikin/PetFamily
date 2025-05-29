using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Disscusion.Domain.Entity;
using PetFamily.Disscusion.Domain.ValueObject;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.SendMessage;

public class SendMessageHandler : ICommandHandler<Guid, SendMessageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SendMessageHandler> _logger;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IValidator<SendMessageCommand> _validator;

    public SendMessageHandler(
        [FromKeyedServices(Modules.Disscusion)] IUnitOfWork unitOfWork,
        ILogger<SendMessageHandler> logger,
        IDiscussionsRepository discussionsRepository,
        IValidator<SendMessageCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _discussionsRepository = discussionsRepository;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Handle(SendMessageCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return Errors.General.ValueIsInvalid().ToErrorList();
        
        var discussionExist = await _discussionsRepository
            .GetById(command.DiscussionId, cancellationToken);
        if (discussionExist.IsFailure)
            return Errors.General.NotFound().ToErrorList();
        
        var message = new Message(
            command.DiscussionId, 
            command.UserId, 
            Content.Create(command.Content).Value);
        
        var result = discussionExist.Value.AddMessage(message);
        if (result.IsFailure)
            return Errors.General.Failure().ToErrorList();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "In discussion with {Id} added message by user {senderId}",
            discussionExist.Value.Id,
            command.UserId);
        
        return discussionExist.Value.Id;
    }
}
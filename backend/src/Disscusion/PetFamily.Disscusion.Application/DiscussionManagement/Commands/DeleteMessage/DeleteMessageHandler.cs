using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.DeleteMessage;

public class DeleteMessageHandler :
    ICommandHandler<Guid, DeleteMessageCommand>
{
    private readonly IValidator<DeleteMessageCommand> _validator;
    private readonly IDiscussionsRepository _discussionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteMessageHandler> _logger;

    public DeleteMessageHandler(
        IValidator<DeleteMessageCommand> validator,
        IDiscussionsRepository discussionRepository,
        [FromKeyedServices(Modules.Disscusion)] IUnitOfWork unitOfWork,
        ILogger<DeleteMessageHandler> logger)
    {
        _validator = validator;
        _discussionRepository = discussionRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteMessageCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(
            command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var discussionExist = await _discussionRepository.GetById(
            command.DiscussionId, cancellationToken);
        if (discussionExist.IsFailure)
            return Errors.General.NotFound(command.DiscussionId).ToErrorList();
        
        var result = discussionExist.Value.RemoveMessage(command.MessageId, command.SenderId);
        if(result.IsFailure)
            return Errors.General.Failure().ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Message {messageId} deleted in discussion with {discussionId}",
            command.MessageId,
            discussionExist.Value.Id);
        
        return command.MessageId;
    }
}
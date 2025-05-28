using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Disscusion.Domain.ValueObject;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.EditMessage;

public class EditMessageHandler :
    ICommandHandler<Guid, EditMessageCommand>
{
    private readonly IValidator<EditMessageCommand> _validator;
    private readonly IDiscussionsRepository _discussionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditMessageHandler> _logger;

    public EditMessageHandler(
        IValidator<EditMessageCommand> validator,
        IDiscussionsRepository discussionRepository,
        [FromKeyedServices(Modules.Disscusion)] IUnitOfWork unitOfWork,
        ILogger<EditMessageHandler> logger)
    {
        _validator = validator;
        _discussionRepository = discussionRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        EditMessageCommand command,
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

        var messageText = Content.Create(command.Text).Value;
        
        discussionExist.Value.EditMessage(command.MessageId, command.SenderId, messageText);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Message {messageId} was edited in discussion with {Id}",
            command.MessageId,
            discussionExist.Value.Id);
        
        return command.MessageId;
    }
}
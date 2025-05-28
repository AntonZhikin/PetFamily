using CSharpFunctionalExtensions;
using PetFamily.Core.Abstractions;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands;
using PetFamily.Disscusion.Application.DiscussionManagement.Commands.Create;
using PetFamily.Disscusion.Contracts;
using PetFamily.Disscusion.Contracts.Request;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Presentation;

public class DiscussionContract : IDiscussionContract
{
    private readonly ICommandHandler<Guid, CreateCommand> _handler;

    public DiscussionContract(ICommandHandler<Guid, CreateCommand> handler)
    {
        _handler = handler;
    }
    public async Task<Result<Guid, ErrorList>> CreateDiscussionForVolunteerRequest(CreateDiscussionRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCommand(request.RequestId, request.ReviewingUserId, request.ApplicantUserId);
        var discussion = await _handler.Handle(command, cancellationToken);
        
        if (discussion.IsFailure)
            return discussion.Error;

        return discussion.Value;
    }
}
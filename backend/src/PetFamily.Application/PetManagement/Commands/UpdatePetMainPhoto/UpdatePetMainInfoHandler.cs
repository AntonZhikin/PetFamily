using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.PetManagement.Commands.UpdatePetMainPhoto;

public class UpdatePetMainInfoHandler : ICommandHandler<Guid, UpdatePetMainInfoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePetMainInfoHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdatePetMainInfoHandler(
        IUnitOfWork unitOfWork, 
        ILogger<UpdatePetMainInfoHandler> logger,
        IVolunteerRepository volunteerRepository
        )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _volunteerRepository = volunteerRepository;
    }
    
    public Task<Result<Guid, ErrorList>> Handle(UpdatePetMainInfoCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
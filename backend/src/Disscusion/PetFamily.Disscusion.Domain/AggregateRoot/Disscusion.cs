using CSharpFunctionalExtensions;
using PetFamily.Disscusion.Domain.Entity;
using PetFamily.Kernel;

namespace PetFamily.Disscusion.Domain.AggregateRoot;

public class Disscusion
{
    // ef core
    private Disscusion() { }

    public Disscusion(List<Guid> users)
    {
        _users = users;
        Id = Guid.NewGuid();
        Status = DisscusionStatus.Open;
    }
    
    public Guid Id { get; private set; }
    
    public Guid RelationId { get; private set; }

    private List<Guid> _users = [];
    
    public IReadOnlyList<Guid> Users => _users;

    private List<Message> _messages = [];
    
    public IReadOnlyList<Message> Messages => _messages;
    
    public DisscusionStatus Status { get; private set; }
    
    // Methods

    public Result<Disscusion, Error> Create(List<Guid> users)
    {
        if (users.Count == 2)
        {
            return new Disscusion(users);
        }
        return Error.Failure("users", "Users must be 1 or more 2");
    }
    
    public void Close()
    {
        Status = DisscusionStatus.Closed;
    }

    public UnitResult<Error> AddMessage(Message message)
    {
        if (Status == DisscusionStatus.Closed)
        {
            return Errors.Disscusion.ClosedDissusion();
        }
        
        if (!_users.Contains(message.SenderId))
        {
            return Errors.Disscusion.UserNotInDisscusion();
        }
        
        _messages.Add(message);
        return Result.Success<Error>();
    }
    
    public UnitResult<Error> RemoveMessage(Message message)
    {
        if (Status == DisscusionStatus.Closed)
        {
            return Errors.Disscusion.ClosedDissusion();
        }
        
        if (!_users.Contains(message.SenderId))
        {
            return Errors.Disscusion.UserNotInDisscusion();
        }
        
        _messages.Remove(message);
        return Result.Success<Error>();
    }
    
    public UnitResult<Error> EditMessage(Guid messageId, Guid userId, string text)
    {
        if (Status == DisscusionStatus.Closed)
        {
            return Errors.Disscusion.ClosedDissusion();
        }
        
        var message = _messages.FirstOrDefault(m => m.Id == messageId);
        if (message == null)
            return Errors.General.NotFound();
        
        if(message.SenderId != userId)
            return Errors.Disscusion.UserNotInDisscusion();
        
        message.EditMessage(text);
        
        return Result.Success<Error>();
    }
    
    
}
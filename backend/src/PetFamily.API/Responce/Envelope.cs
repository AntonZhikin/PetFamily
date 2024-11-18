using PetFamily.Domain.Shared;

namespace PetFamily.API.Responce;

public record ResponceError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

public record Envelope
{
    public object? Result { get; }
    
    public List<ResponceError> Errors { get; }
    
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, IEnumerable<ResponceError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object result = null) =>
        new Envelope(result, []);
    
    public static Envelope Error(List<ResponceError> errors) =>
        new Envelope(null, errors);
    
    
}

using System.Collections;

namespace PetFamily.Kernel;

public class ErrorList : IEnumerable<Error>
{
    private readonly List<Error> _errors;

    public ErrorList(IEnumerable<Error> errors)
    {
        _errors = errors.ToList();
    }
    
    public IEnumerator<Error> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator ErrorList(List<Error> errors)
        => new ErrorList(errors);
    public static implicit operator ErrorList(Error error)
        => new ErrorList([error]);
}
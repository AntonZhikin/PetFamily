using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Pets.Domain.ValueObjects;

public record PhoneNumber
{
    private const string PhoneRegex = @"^[+]{0,1}[0-9]{11}";
    
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("PhoneNumber");
        
        if (Regex.IsMatch(value, PhoneRegex) == false)
            return Errors.General.ValueIsInvalid("PhoneNumber");
        
        return new PhoneNumber(value);
    }
}
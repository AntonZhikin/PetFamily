namespace PetFamily.Application.DTOs.ValueObject;

public class PetPhotoDto
{
    public string PathToStorage { get; set; } = string.Empty;

    public bool IsMain { get; set; } = false;
}
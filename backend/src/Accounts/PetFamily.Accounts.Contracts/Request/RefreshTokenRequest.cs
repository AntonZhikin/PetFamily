namespace PetFamily.Accounts.Contracts.Request;

public record RefreshTokenRequest(string AccessToken, Guid RefreshToken);
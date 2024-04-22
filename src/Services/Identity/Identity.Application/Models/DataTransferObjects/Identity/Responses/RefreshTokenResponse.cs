namespace Identity.Application.Models.DataTransferObjects.Identity.Responses;

public record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken);

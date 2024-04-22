namespace Identity.Application.Models.DataTransferObjects.Identity.Requests;

public record RefreshTokenRequest(
    string AccessToken, 
    string RefreshToken);
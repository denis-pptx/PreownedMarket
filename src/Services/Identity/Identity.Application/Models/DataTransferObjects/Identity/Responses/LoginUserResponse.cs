namespace Identity.Application.Models.DataTransferObjects.Identity.Responses;

public record LoginUserResponse(
    string AccessToken, 
    string RefreshToken);
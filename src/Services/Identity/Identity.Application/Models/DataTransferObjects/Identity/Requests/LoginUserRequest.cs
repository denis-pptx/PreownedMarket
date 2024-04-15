namespace Identity.Application.Models.DataTransferObjects.Identity.Requests;

public record LoginUserRequest(
    string Email,
    string Password);

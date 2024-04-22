namespace Identity.Application.Models.DataTransferObjects.Identity.Requests;

public record RegisterUserRequest(
    string UserName, 
    string Email, 
    string Password);
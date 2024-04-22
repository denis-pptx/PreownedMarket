namespace Identity.Application.Models.DataTransferObjects.Users.Requests;

public record UpdateUserRoleRequest(
    Guid UserId, 
    string NewRole);
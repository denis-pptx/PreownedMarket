namespace Identity.Application.Features.Users.Queries.Models;

public class UserVm
{
    public string Id { get; set; } = string.Empty;
    public string UserName {  get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role {  get; set; } = string.Empty;   
}

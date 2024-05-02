namespace Item.BusinessLogic.Services.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string Role { get; }
}
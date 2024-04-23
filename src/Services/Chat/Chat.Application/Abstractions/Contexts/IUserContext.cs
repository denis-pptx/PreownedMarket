namespace Chat.Application.Abstractions.Contexts;

public interface IUserContext
{
    Guid UserId { get; }
}
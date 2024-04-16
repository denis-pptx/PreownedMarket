namespace Chat.Application.Abstractions;

public interface ICurrentUserService
{
    string? UserId { get; }
}
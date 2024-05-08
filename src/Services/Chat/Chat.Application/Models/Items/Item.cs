namespace Chat.Application.Models.Items;

public record Item(
    Guid Id,
    string Title,
    string? FirstImagePath,
    bool IsActive,
    Guid UserId);
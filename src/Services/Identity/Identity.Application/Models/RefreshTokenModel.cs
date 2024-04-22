namespace Identity.Application.Models;

public record RefreshTokenModel(string Token, DateTime ExpiryTime);
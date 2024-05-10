using Shared.Errors.Messages;

namespace Identity.Application.Exceptions.ErrorMessages;

public static class UserErrorMessages
{
    public static ErrorMessage NotFound => new(
        "User.NotFound",
        "User not found");
    public static ErrorMessage IncorrectCredentials => new(
        "User.IncorrectCredentials",
        "Incorrect Email or Password");

    public static ErrorMessage DeleteYourself => new(
        "User.DeleteYourself",
        "It is impossible to delete yourself");
}
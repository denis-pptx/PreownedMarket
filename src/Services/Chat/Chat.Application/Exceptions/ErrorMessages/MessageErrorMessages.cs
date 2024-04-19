namespace Chat.Application.Exceptions.ErrorMessages;

public static class MessageErrorMessages
{
    public static ErrorMessage UpdateAlienMessage => new(
        "Message.UpdateAlienMessage",
        "Сan't update someone else's message.");
    public static ErrorMessage DeleteAlienMessage => new(
        "Message.DeleteAlienMessage",
        "Сan't delete someone else's message.");
}
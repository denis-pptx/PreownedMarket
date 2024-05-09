using Shared.Errors.Messages;

namespace Chat.Domain.ErrorMessages;

public static class MessageErrorMessages
{
    public static ErrorMessage UpdateAlienMessage => new(
        "Message.UpdateAlienMessage",
        "Сan't update someone else's message.");
    public static ErrorMessage DeleteAlienMessage => new(
        "Message.DeleteAlienMessage",
        "Сan't delete someone else's message.");

    public static ErrorMessage InactiveItem => new(
        "Message.InactiveItem",
        "Can't write / update a message on an inactive item");
}
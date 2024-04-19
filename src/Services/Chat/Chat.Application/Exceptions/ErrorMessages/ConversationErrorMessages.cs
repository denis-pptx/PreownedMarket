namespace Chat.Application.Exceptions.ErrorMessages;

public static class ConversationErrorMessages
{
    public static ErrorMessage AlienConversation => new(
        "Conversation.AlienConversation",
        "Сan't get someone else's conversation.");
}
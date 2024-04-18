namespace Chat.Application.Exceptions.ErrorMessages;

public static class ConversationErrorMessages
{
    public static ErrorMessage AlienConversation => new(
        "Conversation.AlreadyLiked",
        "Сan't get someone else's dialogue.");
}
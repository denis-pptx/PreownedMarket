namespace Chat.Application.Exceptions.ErrorMessages;

public static class ConversationErrorMessages
{
    public static ErrorMessage AlienConversation => new(
        "Conversation.AlienConversation",
        "Сan't get someone else's conversation.");

    public static ErrorMessage MyselfConversation => new(
        "Conversation.AlienConversation",
        "It is impossible to create a dialogue with yourself.");

    public static ErrorMessage AlreadyExists => new(
        "Conversation.AlreadyExists",
        "Such a conversation already exists.");
}
namespace Chat.Application.Models.DataTransferObjects.Conversations.Requests;

public record CheckConversationExistenceRequest(
    string SellerId, 
    string ItemId);
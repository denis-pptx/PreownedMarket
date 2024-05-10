using Shared.Errors.Messages;

namespace Item.DataAccess.ErrorMessages;

public class ItemErrorMessages
{
    public static ErrorMessage StatusFailure => new(
       "Item.StatusFailure",
       "Failed status change");
}

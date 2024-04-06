namespace Item.BusinessLogic.Exceptions.ErrorMessages;

public class ItemErrorMessages
{
    public static ErrorMessage StatusFailure => new(
       "Item.StatusFailure",
       "Failed status change");
}

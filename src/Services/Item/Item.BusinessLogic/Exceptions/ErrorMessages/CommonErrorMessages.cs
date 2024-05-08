namespace Item.BusinessLogic.Exceptions.ErrorMessages;

public static class CommonErrorMessages
{
    public static ErrorMessage InvalidGuid => new(
        "Guid.InvalidValue",
        "Guid value is invalid");
}
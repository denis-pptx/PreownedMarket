using Shared.Errors.Messages;

namespace Item.DataAccess.ErrorMessages;

public static class CommonErrorMessages
{
    public static ErrorMessage InvalidGuid => new(
        "Guid.InvalidValue",
        "Guid value is invalid");
}
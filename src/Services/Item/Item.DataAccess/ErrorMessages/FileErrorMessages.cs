using Shared.Errors.Messages;

namespace Item.DataAccess.ErrorMessages;

public class FileErrorMessages
{
    public static ErrorMessage Empty => new(
        "File.Empty",
        "File is null or empty.");
}

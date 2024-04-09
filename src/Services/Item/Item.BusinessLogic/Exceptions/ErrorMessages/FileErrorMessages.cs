namespace Item.BusinessLogic.Exceptions.ErrorMessages;

public class FileErrorMessages
{
    public static ErrorMessage Empty => new(
        "File.Empty",
        "File is null or empty.");
}

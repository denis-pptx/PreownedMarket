namespace Item.BusinessLogic.Exceptions.ErrorMessages;

public static class CategoryErrorMessages
{
    public static ErrorMessage UniqueName => new(
        "Category.UniqueName",
        "The name of the category must be unique");
}
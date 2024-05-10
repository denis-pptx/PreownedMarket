using Shared.Errors.Messages;

namespace Item.DataAccess.ErrorMessages;

public static class CityErrorMessages
{
    public static ErrorMessage UniqueName => new(
        "City.UniqueName",
        "The name of the city must be unique");
}
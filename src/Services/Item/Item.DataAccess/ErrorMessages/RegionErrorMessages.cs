using Shared.Errors.Messages;

namespace Item.DataAccess.ErrorMessages;

public static class RegionErrorMessages
{
    public static ErrorMessage UniqueName => new(
        "Region.UniqueName",
        "The name of the region must be unique");
}
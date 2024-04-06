using Item.DataAccess.Models;

namespace Item.DataAccess.Data.Initializers.Values;

public static class StatusValues
{
    public static Status UnderReview => new("Under Review", "under-review");
    public static Status Active => new("Active", "active");
    public static Status Rejected => new("Rejected", "rejected");
    public static Status Inactive => new ("Inactive", "inactive");
}
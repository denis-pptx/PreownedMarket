using Item.DataAccess.Models;

namespace Item.DataAccess.Data.Initializers;

public class StatusInitializer
{
    private static List<Status> _statuses => [
        new() { Name = "Under Review", NormalizedName = "under-review" },
        new() { Name = "Active", NormalizedName = "active" },
        new() { Name = "Rejected", NormalizedName = "rejected" },
        new() { Name = "Pending Payment", NormalizedName = "pending-payment" },
        new() { Name = "Inactive", NormalizedName = "inactive" }
   ];

    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        var existingStatusNames = dbContext.Statuses.Select(c => c.NormalizedName);

        var statusedToAdd = _statuses.Where(status => !existingStatusNames.Contains(status.NormalizedName));

        dbContext.Statuses.AddRange(statusedToAdd);

        await dbContext.SaveChangesAsync();
    }
}
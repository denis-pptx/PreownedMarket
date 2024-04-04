using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Data.Initializers;

public static class LocationInitializer
{
    private static List<Region> _regions = [
        new() 
        { 
            Name = "Minsk", 
            Cities = [
                new() { Name = "Minsk" },
                new() { Name = "Borisov" },
                new() { Name = "Zhodino" },
                new() { Name = "Soligorsk" },
                new() { Name = "Slutsk" }
            ] 
        },
        new() 
        { 
            Name = "Brest",
            Cities = [
                new() { Name = "Brest" },
                new() { Name = "Baranovichi" },
                new() { Name = "Kobrin" },
                new() { Name = "Pinsk" },
                new() { Name = "Drohiczyn" }
            ]
        },
        new() 
        {
            Name = "Gomel",
            Cities = [
                new() { Name = "Gomel" },
                new() { Name = "Mozyr" },
                new() { Name = "Svetlogorsk" },
                new() { Name = "Rechitsa" },
                new() { Name = "Kalinkavichy" }
            ]
        },
        new() 
        { 
            Name = "Grodno",
            Cities = [
                new() { Name = "Grodno" },
                new() { Name = "Lida" },
                new() { Name = "Volkovysk" },
                new() { Name = "Slonim" },
                new() { Name = "Svislach" }
            ]
        },
        new() 
        { 
            Name = "Mogilev",
            Cities = [
                new() { Name = "Mogilev" },
                new() { Name = "Bobruisk" },
                new() { Name = "Osipovichi" },
                new() { Name = "Shklov" },
                new() { Name = "Krichev" }
            ]
        },
        new() 
        { 
            Name = "Vitebsk",
            Cities = [
                new() { Name = "Vitebsk" },
                new() { Name = "Orsha" },
                new() { Name = "Polotsk" },
                new() { Name = "Novopolotsk" },
                new() { Name = "Lepel" }
            ]
        },
    ];

    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        foreach (var region in _regions)
        {
            var dbRegion = dbContext.Regions.FirstOrDefault(x => x.Name == region.Name);

            if (dbRegion is null)
            {
                dbContext.Regions.Add(region);
            }
            else
            {
                await dbContext.Entry(dbRegion).Collection(x => x.Cities).LoadAsync();

                var existingCityNames = dbRegion.Cities.Select(c => c.Name);

                var citiesToAdd = region.Cities.Where(city => !existingCityNames.Contains(city.Name));

                dbRegion.Cities.AddRange(citiesToAdd);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

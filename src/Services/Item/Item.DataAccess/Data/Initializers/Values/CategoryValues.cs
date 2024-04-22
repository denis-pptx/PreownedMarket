using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Data.Initializers.Values;

public static class CategoryValues
{
    public static Category RealEstate => new("Real Estate", "real-estate");
    public static Category CarsAndParts => new("Cars and Parts", "cars-and-parts");
    public static Category Appliances => new("Appliances", "appliances");
    public static Category ComputersAndAccessories => new("Computers and Accessories", "computers-and-accessories");
    public static Category PhonesAndTablets => new("Phones and Tablets", "phones-and-tablets");
    public static Category Electronics => new("Electronics", "electronics");
    public static Category MensFashion => new("Men's Fashion", "mens-fashion");
    public static Category WomensFashion => new("Women's Fashion", "womens-fashion");
    public static Category BeautyAndHealth => new("Beauty and Health", "beauty-and-health");
    public static Category BabyAndMomEssentials => new("Baby and Mom Essentials", "baby-and-mom-essentials");
    public static Category Furniture => new("Furniture", "furniture");
    public static Category HomeAccessories => new("Home Accessories", "home-accessories");
    public static Category RepairAndConstruction => new("Repair and Construction", "repair-and-construction");
    public static Category GardenAndOrchard => new("Garden and Orchard", "garden-and-orchard");
    public static Category HobbiesSportsAndTourism => new("Hobbies, Sports, and Tourism", "hobbies-sports-and-tourism");
    public static Category WeddingsAndParties => new("Weddings and Parties", "weddings-and-parties");
    public static Category Pets => new("Pets", "pets");
    public static Category Jobs => new("Jobs", "jobs");
    public static Category Services => new("Services", "services");
    public static Category Other => new("Other", "other");
}
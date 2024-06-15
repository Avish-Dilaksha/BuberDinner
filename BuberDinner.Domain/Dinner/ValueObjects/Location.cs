using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Dinner.ValueObjects;

public sealed class Location : ValueObject
{
    public string Name {get;}
    public string Address {get;}
    public double Latitude {get;}
    public double Longitude {get;}
    public DateTime CreatedDateTime {get;}
    public DateTime UpdatedDateTime {get;}

    private Location(
        string name,
        string address,
        double latitude,
        double longitude,
        DateTime createdDateTime,
        DateTime updatedDateTime
        )
    {
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }
    public static Location CreateUnique(string name,
        string address,
        double latitude,
        double longitude)
    {
        return new(
            name, 
            address,
            latitude,
            longitude,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Address;
        yield return Latitude;
        yield return Longitude;
    }
}
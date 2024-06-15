using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.Entities;
using BuberDinner.Domain.Dinner.Enums;
using BuberDinner.Domain.Dinner.ValueObjects;

namespace BuberDinner.Domain.Dinner;

public sealed class Dinner : AggregateRoot<DinnerId>
{
    private readonly List<Reservation> _reservations = new();
    public string Name {get;}
    public string Description {get;}
    public DateTime StartDateTime {get;}
    public DateTime EndDateTime {get;}
    public DateTime StartedDateTime {get;}
    public DateTime EndedDateTime {get;}
    public DinnerStatus Status {get;}
    public bool IsPublic {get;}
    public int MaxGuests {get;}
    public DateTime CreatedDateTime {get;}
    public DateTime UpdatedDateTime {get;}

    public Location Location {get;}
    public Price Price {get;}

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    private Dinner(
        DinnerId dinnerId,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime startedDateTime,
        DateTime endedDateTime,
        DinnerStatus status,
        bool isPublic,
        int maxGuests,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        Location location,
        Price price
    ) : base(dinnerId)
    {
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        StartedDateTime = startedDateTime;
        EndedDateTime = endedDateTime;
        Status = status;
        IsPublic = isPublic;
        MaxGuests = maxGuests;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        Location = location;
        Price = price;
    }

    public static Dinner Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime startedDateTime,
        DateTime endedDateTime,
        DinnerStatus status,
        bool isPublic,
        int maxGuests,
        Location location,
        Price price
    )
    {
        return new Dinner(
            DinnerId.CreateUnique(),
            name,
            description,
            startDateTime,
            endDateTime,
            startedDateTime,
            endedDateTime,
            status,
            isPublic,
            maxGuests,
            DateTime.UtcNow,
            DateTime.UtcNow,
            location,
            price
        );
    }

}
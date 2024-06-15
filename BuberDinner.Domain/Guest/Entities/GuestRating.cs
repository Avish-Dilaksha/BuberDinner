using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;

namespace BuberDinner.Domain.Guest.Entities;

public sealed class GuestRating : Entity<GuestRatingId>
{
    public HostId HostId {get;}
    public DinnerId DinnerId {get;}
    public decimal Rate {get;}
    public DateTime CreatedDateTime {get;}
    public DateTime UpdatedDateTime {get;}

    private GuestRating(
        GuestRatingId ratingId, 
        decimal rate, 
        HostId hostId, 
        DinnerId dinnerId, 
        DateTime createdDate,
        DateTime updatedDate
        )
        : base(ratingId)
    {
        Rate = rate;
        HostId = hostId;
        DinnerId = dinnerId;
        CreatedDateTime = createdDate;
        UpdatedDateTime = updatedDate;
    }

    public static GuestRating Create(
        decimal rate, 
        HostId hostId, 
        DinnerId dinnerId)
    {
        return new(
            GuestRatingId.CreateUnique(),
            rate,
            hostId,
            dinnerId,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }
}
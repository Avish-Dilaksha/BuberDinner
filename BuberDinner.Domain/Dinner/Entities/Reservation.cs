using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.Enums;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;

namespace BuberDinner.Domain.Dinner.Entities;

public sealed class Reservation : Entity<ReservationId>
{
    public string Name {get;}
    public ReservationStatus ReservationStatus {get;}
    public int GuestCount {get;}
    public GuestId GuestId {get;}
    public BillId BillId {get;}
    public DateTime CreatedDateTime {get;}
    public DateTime UpdatedDateTime {get;}
    public DateTime ArrivalDateTime {get;}

    private Reservation(
        ReservationId reservationId, 
        string name, 
        ReservationStatus reservationStatus,
        int guestCount,
        GuestId guestId,
        BillId billId,
        DateTime arrivalDateTime,
        DateTime createdDateTime,
        DateTime updatedDateTime
        )
        : base(reservationId)
    {
        Name = name;
        ReservationStatus = reservationStatus;
        GuestCount = guestCount;
        GuestId = guestId;
        BillId = billId;
        ArrivalDateTime = arrivalDateTime;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Reservation Create(
        string name, 
        ReservationStatus reservationStatus,
        int guestCount,
        GuestId guestId,
        BillId billId,
        DateTime arrivalDateTime
        )
    {
        return new(
            ReservationId.CreateUnique(), 
            name, 
            reservationStatus,
            guestCount,
            guestId,
            billId,
            arrivalDateTime,
            DateTime.UtcNow,
            DateTime.UtcNow
            );
    }
}
using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Bill.ValueObjects;

public sealed class Price : ValueObject
{
    public decimal Amount {get;}
    public string Currency {get;}

    private Price(
        decimal amount,
        string currency
        )
    {
        Amount = amount;
        Currency = currency;
    }
    public static Price CreateUnique(
        decimal amount,
        string currency)
    {
        return new(
            amount,
            currency
            );
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
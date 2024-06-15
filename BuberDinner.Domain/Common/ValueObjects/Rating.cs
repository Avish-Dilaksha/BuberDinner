using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Common.ValueObjects;

public sealed class Rating : ValueObject
{
    public double Value {get; private set;}
    public int NumRatings { get; private set;}

    private Rating(double value, int numRatings)
    {
        NumRatings = numRatings;
        Value = value;
    }
    public static Rating CreateNew(double rating = 0, int numRatings = 0)
    {
        return new Rating(rating, numRatings);
    }

    public void AddNewRating(Rating rating)
    {
        Value = ((Value * NumRatings) + rating.Value) / ++NumRatings;
    }

    public void RemoveRating(Rating rating)
    {
        Value = ((Value * NumRatings) - rating.Value) / --NumRatings;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
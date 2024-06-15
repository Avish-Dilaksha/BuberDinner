using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Common.ValueObjects;

public sealed class AverageRating : ValueObject
{
    public double Value {get; private set;}
    public int NumRatings { get; private set;}

    private AverageRating(double value, int numRatings)
    {
        NumRatings = numRatings;
        Value = value;
    }
    public static AverageRating CreateNew(double rating = 0, int numRatings = 0)
    {
        return new AverageRating(rating, numRatings);
    }

    public void AddNewRating(AverageRating rating)
    {
        Value = ((Value * NumRatings) + rating.Value) / ++NumRatings;
    }

    public void RemoveRating(AverageRating rating)
    {
        Value = ((Value * NumRatings) - rating.Value) / --NumRatings;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
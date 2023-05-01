namespace Wenab.Api.Models;

public sealed class SpendingSummary
{
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public IReadOnlyList<OwnerSummary> OwnerSummaries { get; } = new List<OwnerSummary>
    {
        new(Owner.Rachel),
        new(Owner.Steve)
    };

    public OwnerSummary this[Owner owner] => OwnerSummaries.First(summary => summary.Owner == owner);
}
namespace Wenab.Api.Models;

public sealed class Ledger
{
    public IReadOnlyCollection<Transaction> Transactions { get; init; }
}
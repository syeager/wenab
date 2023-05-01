namespace Wenab.Api.Models;

public sealed class Snapshot
{
    public IReadOnlyDictionary<Guid, Account> Accounts { get; }
    public IReadOnlyDictionary<Guid, CategoryGroup> CategoryGroups { get; }
    public IReadOnlyDictionary<Guid, Transaction> Transactions { get; }

    public Snapshot(IReadOnlyDictionary<Guid, Account> accounts, IReadOnlyDictionary<Guid, CategoryGroup> categoryGroups, IReadOnlyDictionary<Guid, Transaction> transactions)
    {
        Accounts = accounts;
        CategoryGroups = categoryGroups;
        Transactions = transactions;
    }
}
namespace Wenab.Api.Models;

public sealed class OwnerSummary
{
    private readonly List<TransactionSummary> transactions = new();
    private readonly Dictionary<Account, long> accountAmounts = new();

    public Owner Owner { get; }
    public IReadOnlyList<TransactionSummary> Transactions => transactions;
    public IReadOnlyDictionary<Account, long> AccountAmounts => accountAmounts;
    public long TotalAmount => Transactions.Sum(t => t.Amount);

    public OwnerSummary(Owner owner)
    {
        Owner = owner;
    }

    public override string ToString() => Owner.ToString();

    public void Add(TransactionSummary summary)
    {
        transactions.Add(summary);

        accountAmounts.TryGetValue(summary.Transaction.Account, out var amount);
        amount += summary.Amount;
        accountAmounts[summary.Transaction.Account] = amount;
    }
}
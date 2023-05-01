using LittleByte.Common.Domain;

namespace Wenab.Api.Models;

public sealed class Transaction
{
    public Id<Transaction> Id { get; init; }
    public DateTime Date { get; init; }
    public long Amount { get; init; }
    public Category? Category { get; init; }
    public Account Account { get; init; }
    public string Memo { get; init; }
    public string PayeeName { get; init; }

    public override string ToString() => $"{PayeeName}: {Memo}";
}
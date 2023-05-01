using System.ComponentModel.DataAnnotations;
using Wenab.Api.Models;

namespace Wenab.Api.Dtos;

public sealed class OwnerSummaryDto
{
    [Required] public Dictionary<Guid, long> AccountAmounts { get; init; }

    [Required] public Owner Owner { get; init; }

    [Required] public long TotalAmount { get; init; }

    [Required] public TransactionSummaryDto[] TransactionSummaries { get; init; }

    public static OwnerSummaryDto Create(OwnerSummary ownerSummary)
    {
        var dto = new OwnerSummaryDto
        {
            Owner = ownerSummary.Owner,
            TotalAmount = ownerSummary.TotalAmount,
            AccountAmounts = ownerSummary.AccountAmounts.ToDictionary(aa => aa.Key.Id.Value, aa => aa.Value),
            TransactionSummaries = ownerSummary.Transactions.Select(t => new TransactionSummaryDto(
                t.Transaction.Id,
                t.Amount,
                t.Transaction.Account.Id,
                t.Transaction.PayeeName,
                t.SplitConfig.Name,
                t.Transaction.Category?.Id,
                t.Transaction.Memo)).ToArray()
        };

        return dto;
    }
}
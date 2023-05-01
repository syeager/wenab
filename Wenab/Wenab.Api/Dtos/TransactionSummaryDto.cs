using System.ComponentModel.DataAnnotations;

namespace Wenab.Api.Dtos;

public sealed class TransactionSummaryDto
{
    [Required]
    public Guid TransactionId { get; }
    [Required]
    public long OwnerAmount { get; }
    [Required]
    public Guid Account { get; }
    [Required]
    public string Payee { get; }
    [Required]
    public string SplitConfigName { get; }
    public Guid? Category { get; }
    [Required]
    public string Memo { get; }

    public TransactionSummaryDto(Guid transactionId, long ownerAmount, Guid account, string payee, string splitConfigName, Guid? category, string? memo)
    {
        TransactionId = transactionId;
        OwnerAmount = ownerAmount;
        Account = account;
        Payee = payee;
        SplitConfigName = splitConfigName;
        Category = category;
        Memo = memo;
    }
}
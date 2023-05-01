namespace Wenab.Api.Models;

public record TransactionSummary(Transaction Transaction, SplitConfig SplitConfig, long Amount);
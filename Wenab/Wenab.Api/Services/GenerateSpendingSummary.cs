using Wenab.Api.Models;

namespace Wenab.Api.Services;

public class GenerateSpendingSummary
{
    public SpendingSummary Generate(BudgetConfig budgetConfig, Snapshot snapshot, DateTime fromDate,
        DateTime toDate)
    {
        var spendingSummary = new SpendingSummary
        {
            FromDate = fromDate,
            ToDate = toDate,
        };

        var transactions = snapshot.Transactions
            .Where(t => t.Value.Category is not null && t.Value.Date >= fromDate && t.Value.Date <= toDate)
            .Select(t => t.Value)
            .ToArray();

        foreach (var transaction in transactions)
        {
            var categoryGroup = snapshot.CategoryGroups.FirstOrDefault(cg => cg.Value.Categories.Contains(transaction.Category)).Value;
            var splitConfig = budgetConfig.GetSplitConfig(categoryGroup);

            if (splitConfig is null)
            {
                continue;
            }

            foreach (var portion in splitConfig.Split.Portions)
            {
                var ownerSummary = spendingSummary[portion.Owner];
                var amount = (long)(portion.Percent * transaction.Amount);
                var transactionSummary = new TransactionSummary(transaction, splitConfig, amount);
                ownerSummary.Add(transactionSummary);
            }
        }

        return spendingSummary;
    }
}
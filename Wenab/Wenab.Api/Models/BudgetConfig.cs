namespace Wenab.Api.Models;

public sealed class BudgetConfig
{
    public SplitConfig[] Splits { get; init; }
    public AccountConfig[] Accounts { get; init; }

    public SplitConfig? GetSplitConfig(CategoryGroup group) => Splits.FirstOrDefault(split => split.CategoryGroups.Contains(group.Id));
}
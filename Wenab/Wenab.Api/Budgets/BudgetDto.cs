namespace Wenab.Api.Budgets;

public sealed record CategoryDto(string Name, long Balance);

public sealed record BudgetDto(IReadOnlyList<CategoryDto> Categories);
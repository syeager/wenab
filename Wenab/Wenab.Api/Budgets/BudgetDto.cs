using System.ComponentModel.DataAnnotations;
using Wenab.Api.Categories;

namespace Wenab.Api.Budgets;

public sealed record BudgetDto([Required] IReadOnlyList<CategoryDto> Categories);
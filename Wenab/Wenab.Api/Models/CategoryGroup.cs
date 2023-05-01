using LittleByte.Common.Domain;

namespace Wenab.Api.Models;

public sealed class CategoryGroup
{
    public Id<CategoryGroup> Id { get; init; }
    public string Name { get; init; }
    public IReadOnlyList<Category> Categories { get; init; }

    public CategoryGroup(Id<CategoryGroup> id, string name, IReadOnlyList<Category> categories)
    {
        Id = id;
        Name = name;
        Categories = categories;
    }

    public override string ToString() => Name;
}
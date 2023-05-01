using LittleByte.Common.Domain;

namespace Wenab.Api.Models;

public sealed class Category
{
    public Id<Category> Id { get; init; }
    public string Name { get; init; }

    public Category(Id<Category> id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;
}
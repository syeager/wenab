using System.ComponentModel.DataAnnotations;

namespace Wenab.Api.Dtos;

public sealed class CategoryGroupDto
{
    [Required]
    public Guid Id { get; }
    [Required]
    public string Name { get; }
    [Required]
    public CategoryDto[] Categories { get; }

    public CategoryGroupDto(Guid id, string name, CategoryDto[] categories)
    {
        Id = id;
        Name = name;
        Categories = categories;
    }
}
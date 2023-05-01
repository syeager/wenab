using System.ComponentModel.DataAnnotations;

namespace Wenab.Api.Dtos;

public sealed class CategoryDto
{
    [Required]
    public Guid Id { get; }
    [Required]
    public string Name { get; }

    public CategoryDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
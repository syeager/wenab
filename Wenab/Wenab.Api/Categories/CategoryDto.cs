using System.ComponentModel.DataAnnotations;

namespace Wenab.Api.Categories;

public sealed record CategoryDto([Required] string Name, [Required] long Balance);
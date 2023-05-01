using System.ComponentModel.DataAnnotations;

namespace Wenab.Api.Dtos;

public sealed class SnapshotDto
{
    [Required]
    public AccountDto[] Accounts { get; }
    [Required]
    public CategoryGroupDto[] CategoryGroups { get; }

    public SnapshotDto(AccountDto[] accounts, CategoryGroupDto[] categoryGroups)
    {
        Accounts = accounts;
        CategoryGroups = categoryGroups;
    }
}
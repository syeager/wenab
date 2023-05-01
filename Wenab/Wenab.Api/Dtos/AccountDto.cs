using System.ComponentModel.DataAnnotations;
using Wenab.Api.Models;

namespace Wenab.Api.Dtos;

public sealed class AccountDto
{
    [Required]
    public Guid Id { get; }
    [Required]
    public string Name { get; }
    [Required]
    public Owner Owner { get; }
    [Required]
    public Guid BackingAccount { get; }

    public AccountDto(Guid id, string name, Owner owner, Guid backingAccount)
    {
        Id = id;
        Name = name;
        Owner = owner;
        BackingAccount = backingAccount;
    }
}
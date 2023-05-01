namespace Wenab.Api.Models;

public sealed class AccountConfig
{
    public Guid Id { get; init; }
    public Owner Owner { get; init; }
    public Guid BackingAccount { get; init; }
}
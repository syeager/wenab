using LittleByte.Common.Domain;

namespace Wenab.Api.Models;

public sealed class Account
{
    public Id<Account> Id { get; }
    public string Name { get; }
    public Owner Owner { get; }
    public Id<Account> BackingAccount { get; }

    public Account(Id<Account> id, string name, Owner owner, Id<Account> backingAccount)
    {
        Id = id;
        Name = name;
        Owner = owner;
        BackingAccount = backingAccount;
    }

    public override string ToString() => Name;
}
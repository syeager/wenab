import { Account } from "./Account";
import { Owner } from "./Owner";

export class Accounts {
  private readonly accounts: { [id: string]: Account };
  public readonly accountIds: string[];

  constructor(accounts: Account[]) {
    this.accounts = {};
    this.accountIds = [];

    accounts.forEach((a) => {
      this.accountIds.push(a.id);
      this.accounts[a.id] = a;
    });
  }

  public get(id: string): Account {
    return this.accounts[id];
  }

  public isOwner(account: Account, owner: Owner): boolean {
    const backingAccount = this.accounts[account.backingAccountId];
    const isOwner = backingAccount.owner == owner;
    return isOwner;
  }
}

import { Owner } from "./Owner";
import { Transaction } from "./Transaction";

export class Account {
  public readonly id: string;
  public readonly name: string;
  public readonly owner: Owner;
  public readonly backingAccountId: string;
  public readonly transactions: Transaction[];

  constructor(
    id: string,
    name: string,
    owner: Owner,
    backingAccountId: string,
    transactions: Transaction[]
  ) {
    this.id = id;
    this.name = name;
    this.owner = owner;
    this.backingAccountId = backingAccountId;
    this.transactions = transactions;
  }
}

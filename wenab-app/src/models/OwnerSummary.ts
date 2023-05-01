import { Wenab } from "generated/wenabClient";
import { Owner } from "./Owner";
import { Accounts } from "./Accounts";
import { Account } from "./Account";
import { GetOwner } from "./Owners";
import { Transaction, createTransaction } from "./Transaction";
import { CategoryGroup, createCategoryGroup } from "./CategoryGroup";

export class OwnerSummary {
  public readonly owner: Owner;
  public readonly accounts: Accounts;
  public readonly categoryGroups: CategoryGroup[];

  constructor(
    owner: Owner,
    accounts: Accounts,
    categoryGroups: CategoryGroup[]
  ) {
    this.owner = owner;
    this.accounts = accounts;
    this.categoryGroups = categoryGroups;
  }
}

export function createOwnerSummary(
  ownerData: Wenab.OwnerSummaryDto,
  snapshot: Wenab.SnapshotDto
): OwnerSummary {
  const owner = GetOwner(ownerData.owner);
  const transactions = createTransactions(ownerData.transactionSummaries);
  const accounts = createAccounts(snapshot, ownerData, transactions);
  const categoryGroups = createCategoryGroups(
    snapshot,
    Object.values(transactions)
  );
  const ownerSummary = new OwnerSummary(owner, accounts, categoryGroups);
  return ownerSummary;
}

function createTransactions(
  transactionData: Wenab.TransactionSummaryDto[]
): Record<string, Transaction> {
  const transactions = transactionData.map(createTransaction);
  const record: Record<string, Transaction> = {};
  transactions.forEach((t) => (record[t.id] = t));
  return record;
}

function createAccounts(
  snapshot: Wenab.SnapshotDto,
  ownerData: Wenab.OwnerSummaryDto,
  transactions: Record<string, Transaction>
): Accounts {
  const accountData = snapshot.accounts.map(
    (a) =>
      new Account(
        a.id,
        a.name,
        GetOwner(a.owner),
        a.backingAccount,
        ownerData.transactionSummaries
          .filter((t) => t.account == a.id)
          .map((t) => transactions[t.transactionId])
      )
  );
  const accounts = new Accounts(accountData);
  return accounts;
}

function createCategoryGroups(
  snapshot: Wenab.SnapshotDto,
  transactions: Transaction[]
): CategoryGroup[] {
  const categoryGroups = snapshot.categoryGroups.map((cg) =>
    createCategoryGroup(cg, -1, transactions)
  );
  return categoryGroups;
}

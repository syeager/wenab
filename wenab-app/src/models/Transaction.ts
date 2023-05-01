import { Wenab } from "generated/wenabClient";

export class Transaction {
  public readonly id: string;
  public readonly payee: string;
  public readonly memo: string;
  public readonly categoryId: string | undefined;
  public readonly ownerAmount: number;

  constructor(
    id: string,
    payee: string,
    memo: string,
    categoryId: string | undefined,
    ownerAmount: number
  ) {
    this.id = id;
    this.payee = payee;
    this.memo = memo;
    this.categoryId = categoryId;
    this.ownerAmount = ownerAmount;
  }
}

export function createTransaction(
  data: Wenab.TransactionSummaryDto
): Transaction {
  return new Transaction(
    data.transactionId,
    data.payee,
    data.memo,
    data.category,
    data.ownerAmount
  );
}

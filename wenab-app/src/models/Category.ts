import { Wenab } from "generated/wenabClient";
import { Transaction } from "./Transaction";

export class Category {
  public readonly id: string;
  public readonly name: string;
  public readonly transactions: Transaction[];

  constructor(id: string, name: string, transactions: Transaction[]) {
    this.id = id;
    this.name = name;
    this.transactions = transactions;
  }
}

export function createCategory(
  data: Wenab.CategoryDto,
  transactions: Transaction[]
): Category {
  return new Category(
    data.id,
    data.name,
    transactions.filter((t) => t.categoryId == data.id)
  );
}

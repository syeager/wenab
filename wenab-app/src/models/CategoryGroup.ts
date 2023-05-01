import { Wenab } from "generated/wenabClient";
import { Category, createCategory } from "./Category";
import { Transaction } from "./Transaction";

export class CategoryGroup {
  public readonly id: string;
  public readonly name: string;
  public readonly ownerAmount: number;
  public readonly categories: Category[];

  constructor(
    id: string,
    name: string,
    ownerAmount: number,
    categories: Category[]
  ) {
    this.id = id;
    this.name = name;
    this.ownerAmount = ownerAmount;
    this.categories = categories;
  }
}

export function createCategoryGroup(
  data: Wenab.CategoryGroupDto,
  ownerAmount: number,
  transactions: Transaction[]
): CategoryGroup {
  const categories = data.categories.map((c) =>
    createCategory(c, transactions)
  );
  return new CategoryGroup(data.id, data.name, ownerAmount, categories);
}

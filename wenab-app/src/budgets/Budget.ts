import { Category } from "@/categories";

export class Budget {
  readonly categories: Category[];

  constructor(categories: Category[]) {
    this.categories = categories;
  }
}

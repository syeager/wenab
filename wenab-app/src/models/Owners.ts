import { Owner } from "./Owner";

const owners: Record<number, Owner> = {
  1: new Owner("Rachel"),
  2: new Owner("Steve"),
};

export function GetOwner(id: number): Owner {
  return owners[id];
}

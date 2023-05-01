import { Wenab } from "generated/wenabClient";
import { OwnerSummary } from "./OwnerSummary";
import { Owner } from "./Owner";

export class Ledger {
  // TODO: Delete
  public readonly responseSummary: Wenab.ResponseDto;

  public readonly ownerSummaries: OwnerSummary[];

  constructor(
    responseSummary: Wenab.ResponseDto,
    ownerSummaries: OwnerSummary[]
  ) {
    this.responseSummary = responseSummary;
    this.ownerSummaries = ownerSummaries;
  }

  public getOwnerSummary(owner: Owner): OwnerSummary {
    return this.ownerSummaries.find((os) => os.owner == owner)!;
  }
}

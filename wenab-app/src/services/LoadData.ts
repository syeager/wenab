import { Wenab } from "generated/wenabClient";
import { Ledger, OwnerSummary, createOwnerSummary } from "models";

export async function LoadData(month: number, year: number): Promise<Ledger> {
  const client = new Wenab.Client();
  const response = await client.getSpendingSummary_Get(month, year);

  if (response.isError || response.obj == null) {
    console.error(response.message);
    throw alert(response.message);
  }

  const data = response.obj;
  const ownerSummaries = createOwnerSummaries(data);
  const ledger = new Ledger(data, ownerSummaries);
  return ledger;
}

function createOwnerSummaries(data: Wenab.ResponseDto): OwnerSummary[] {
  const ownerSummaries = data.spendingSummary.ownerSummaries.map((os) =>
    createOwnerSummary(os, data.snapshot)
  );
  return ownerSummaries;
}

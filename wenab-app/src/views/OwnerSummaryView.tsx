import { OwnerSummary } from "models";

type Props = {
  ownerSummary: OwnerSummary;
};

export function OwnerSummaryView(props: Props): JSX.Element {
  const summary = props.ownerSummary;

  return <div>{summary.owner.name}</div>;
}

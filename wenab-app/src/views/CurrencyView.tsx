type Props = {
  value: number;
};

export function CurrencyView(props: Props): JSX.Element {
  const { value } = props;

  const formattedValue =
    value == null || value == 0
      ? "$0.000"
      : `${value < 0 ? "-" : ""}$${Math.abs(value / 1000).toLocaleString(
          undefined,
          { minimumFractionDigits: 3 }
        )}`;

  return <span>{formattedValue}</span>;
}

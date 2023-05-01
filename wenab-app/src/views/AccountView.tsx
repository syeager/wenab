import { Account } from "models/Account";
import { CurrencyView } from "./CurrencyView";

type Props = {
  account: Account;
  isOwner: boolean;
};

export function AccountView(props: Props): JSX.Element {
  const { account, isOwner } = props;

  let total = 0;
  let owe = 0;

  account.transactions.forEach((t) => {
    total += t.ownerAmount;
    if (!isOwner) {
      owe += t.ownerAmount;
    }
  });

  return (
    <tr>
      <td className="text-start">{account.name}</td>
      <td className="text-end">
        <CurrencyView value={total} />
      </td>
      <td className="text-end">
        <CurrencyView value={owe} />
      </td>
    </tr>
  );
}

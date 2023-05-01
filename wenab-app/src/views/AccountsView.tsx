import { Table } from "react-bootstrap";
import { Accounts } from "models/Accounts";
import { AccountView } from "./AccountView";
import { Owner } from "models";

type Props = {
  accounts: Accounts;
  owner: Owner;
};

export function AccountsView(props: Props): JSX.Element {
  const { accounts, owner } = props;

  const accountViews = accounts.accountIds.map((id) => {
    const account = accounts.get(id);
    const isOwner = accounts.isOwner(account, owner);
    return <AccountView account={account} key={id} isOwner={isOwner} />;
  });

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th className="text-start w-50">Account</th>
          <th className="text-end w-25">Delta</th>
          <th className="text-end w-25">Owe</th>
        </tr>
      </thead>
      <tbody>{accountViews}</tbody>
    </Table>
  );
}

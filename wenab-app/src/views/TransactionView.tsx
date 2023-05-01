import { Container, ListGroup } from "react-bootstrap";
import { CurrencyView } from "./CurrencyView";
import { Transaction } from "models/Transaction";

type Props = {
  transaction: Transaction;
};

export function TransactionView(props: Props): JSX.Element {
  const { payee, memo, ownerAmount } = props.transaction;

  return (
    <ListGroup.Item className="px-1">
      <Container className="d-flex justify-content-between px-0" fluid>
        <span>{payee}</span>
        <span>{memo}</span>
        <CurrencyView value={ownerAmount} />
      </Container>
    </ListGroup.Item>
  );
}

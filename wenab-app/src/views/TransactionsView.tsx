import { Container, Row, Col, ListGroup } from "react-bootstrap";
import { TransactionView } from "./TransactionView";
import { Transaction } from "models/Transaction";

type Props = {
  transactions: Transaction[];
};

export function TransactionViews(props: Props): JSX.Element {
  const { transactions } = props;

  const transactionViews = transactions.map((t) => (
    <TransactionView transaction={t} key={t.id} />
  ));

  return (
    <Container className="px-0">
      <Row className="mx-0">
        <Col className="px-0">
          <b>Transactions</b>
          <ListGroup>{transactionViews}</ListGroup>
        </Col>
      </Row>
    </Container>
  );
}

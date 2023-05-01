import { Category } from "models/Category";
import { Container, ListGroup } from "react-bootstrap";
import { CurrencyView } from "./CurrencyView";

type Props = {
  category: Category;
  isActive: boolean;
  onClick: (id: string) => void;
};

export function CategoryView(props: Props): JSX.Element {
  const { isActive, onClick } = props;
  const { id, name, transactions } = props.category;

  const ownerAmount = transactions.reduce((s, c) => s + c.ownerAmount, 0);

  return (
    <ListGroup.Item
      action
      active={isActive}
      className="px-1"
      onClick={() => onClick(id)}
    >
      <Container className="d-flex justify-content-between px-0" fluid>
        <span>{name}</span>
        <CurrencyView value={ownerAmount} />
      </Container>
    </ListGroup.Item>
  );
}

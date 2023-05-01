import { CategoryGroup } from "models/CategoryGroup";
import { Container, ListGroup } from "react-bootstrap";
import { CurrencyView } from "./CurrencyView";

type Props = {
  categoryGroup: CategoryGroup;
  isActive: boolean;
  onClick: (id: string) => void;
};

export function CategoryGroupView(props: Props): JSX.Element {
  const { isActive, onClick } = props;
  const { categories, id, name } = props.categoryGroup;

  const ownerAmount = categories.reduce(
    (sum, current) =>
      sum + current.transactions.reduce((s, c) => s + c.ownerAmount, 0),
    0
  );

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

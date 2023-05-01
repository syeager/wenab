import { CategoryGroup } from "models/CategoryGroup";
import { CategoryGroupView } from "./CategoryGroupView";
import { Col, Container, ListGroup, Row } from "react-bootstrap";
import { useState } from "react";

type Props = {
  categoryGroups: CategoryGroup[];
  onChange: (categoryGroup: CategoryGroup) => void;
};

export function CategoryGroupsView(props: Props): JSX.Element {
  const { categoryGroups, onChange } = props;

  const [active, setActive] = useState(categoryGroups[0].id);

  const categoryGroupViews = categoryGroups.map((cg) => (
    <CategoryGroupView
      categoryGroup={cg}
      key={cg.id}
      isActive={cg.id == active}
      onClick={(id) => {
        setActive(id);
        onChange(cg);
      }}
    />
  ));

  return (
    <Container className="px-0">
      <Row className="mx-0">
        <Col className="px-0">
          <b>Groups</b>
          <ListGroup>{categoryGroupViews}</ListGroup>
        </Col>
      </Row>
    </Container>
  );
}

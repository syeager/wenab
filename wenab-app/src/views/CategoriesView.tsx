import { Category } from "models/Category";
import { CategoryView } from "./CategoryView";
import { Col, Container, ListGroup, Row } from "react-bootstrap";
import { useState } from "react";

type Props = {
  categories: Category[];
  onChange: (category: Category) => void;
};

export function CategoriesView(props: Props): JSX.Element {
  const { categories, onChange } = props;

  const [active, setActive] = useState(categories[0].id);

  const categoryViews = categories.map((c) => (
    <CategoryView
      category={c}
      key={c.id}
      isActive={c.id == active}
      onClick={(id) => {
        setActive(id);
        onChange(c);
      }}
    />
  ));

  return (
    <Container className="px-0">
      <Row className="mx-0">
        <Col className="px-0">
          <b>Categories</b>
          <ListGroup>{categoryViews}</ListGroup>
        </Col>
      </Row>
    </Container>
  );
}

import { Ledger } from "models";
import { Col, Container, Row } from "react-bootstrap";
import "./Layout.css";
import { useEffect, useState } from "react";
import { AccountsView } from "./AccountsView";
import { Toolbar } from "./Toolbar";
import { CategoryGroupsView } from "./CategoryGroupsView";
import { CategoriesView } from "./CategoriesView";
import { TransactionViews } from "./TransactionsView";

type Props = {
  ledger: Ledger;
};

export function Layout(props: Props): JSX.Element {
  const { ledger } = props;

  const [activeOwner, setActiveOwner] = useState(ledger.ownerSummaries[0]);
  const [activeGroup, setActiveGroup] = useState(activeOwner.categoryGroups[0]);
  const [activeCategory, setActiveCategory] = useState(
    activeGroup.categories[0]
  );

  useEffect(() => setActiveGroup(activeOwner.categoryGroups[0]), [activeOwner]);
  useEffect(() => setActiveCategory(activeGroup.categories[0]), [activeGroup]);

  return (
    <Container>
      <Row className="toolbar">
        <Toolbar
          currentOwner={activeOwner.owner}
          onOwnerChange={(o) => setActiveOwner(ledger.getOwnerSummary(o))}
        />
      </Row>
      <Row className="accounts">
        <Col>
          <AccountsView
            accounts={activeOwner.accounts}
            owner={activeOwner.owner}
          />
        </Col>
      </Row>
      <Row>
        <Col className="categroyGroups" lg="3">
          <CategoryGroupsView
            categoryGroups={activeOwner.categoryGroups}
            onChange={setActiveGroup}
          />
        </Col>
        <Col className="categroyGroups" lg="3">
          <CategoriesView
            categories={activeGroup.categories}
            onChange={setActiveCategory}
          />
        </Col>
        <Col className="transactions">
          <TransactionViews transactions={activeCategory.transactions} />
        </Col>
      </Row>
    </Container>
  );
}

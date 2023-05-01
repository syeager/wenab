import { useEffect, useState } from "react";
import "./App.css";
import { Wenab } from "./generated/wenabClient";
import Table from "react-bootstrap/Table";
import { Col, Nav, Row, Tab } from "react-bootstrap";
import { Ledger } from "models";
import { LoadData } from "services";
import { Layout } from "views";

function App() {
  const [useNew, setUseNew] = useState(true);
  const [isLoading, setIsLoading] = useState(true);
  const [ledger, setLedger] = useState(undefined as unknown as Ledger);

  useEffect(() => {
    LoadData(4, 2023).then((l) => {
      setLedger(l);
      setIsLoading(false);
    });
  }, []);

  const body = isLoading ? (
    <p>Loading...</p>
  ) : useNew ? (
    <Layout ledger={ledger} />
  ) : (
    renderOld(ledger.responseSummary)
  );

  return (
    <div className="App">
      <button onClick={() => setUseNew(!useNew)}>Swap</button>
      {body}
    </div>
  );
}

export default App;

function renderOld(responseSummary: Wenab.ResponseDto): JSX.Element {
  const ownerSummaries = responseSummary.spendingSummary.ownerSummaries.map(
    (os) => <div key={os.owner}>{renderOwnerSummary(os, responseSummary)}</div>
  );

  return (
    <div>
      <p>
        {responseSummary.spendingSummary.fromDate?.toString()} -
        {responseSummary.spendingSummary.toDate?.toString()}
      </p>
      <div>{ownerSummaries}</div>
    </div>
  );
}

function renderOwnerSummary(
  ownerSummary: Wenab.OwnerSummaryDto,
  responseSummary: Wenab.ResponseDto
): JSX.Element {
  const name = ownerSummary.owner == 1 ? "Rachel" : "Steve";

  const snapshot = responseSummary.snapshot;

  const accountViews = snapshot.accounts
    .sort((a, b) => a.name.localeCompare(b.name))
    .map((a) =>
      renderAccount(
        ownerSummary.owner,
        snapshot,
        a,
        ownerSummary.transactionSummaries
      )
    );

  const categories = snapshot.categoryGroups
    .flatMap((cg) => cg.categories)
    .sort((a, b) => a.name.localeCompare(b.name));

  const categoryTotals: {
    [categoryId: string]: number;
  } = {};

  const categoryTransactionViews: {
    [categoryId: string]: JSX.Element[];
  } = {};

  const transactionSummaries = ownerSummary.transactionSummaries.sort(
    (a, b) => a.ownerAmount - b.ownerAmount
  );

  transactionSummaries.forEach((t) => {
    const c = t.category!;
    let views = categoryTransactionViews[c];
    if (views == null) {
      views = categoryTransactionViews[c] = [];
    }
    views.push(renderTransaction(t));

    if (categoryTotals[c] == null) {
      categoryTotals[c] = 0;
    }
    categoryTotals[c] += t.ownerAmount;
  });

  const categoryPages = categories.map((c) => {
    return (
      <Tab.Pane eventKey={c.id} key={c.id}>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th className="text-start">Payee</th>
              <th className="text-start">Memo</th>
              <th className="text-end">Amount</th>
            </tr>
          </thead>
          <tbody>{categoryTransactionViews[c.id]}</tbody>
        </Table>
      </Tab.Pane>
    );
  });

  const categoryTabs = categories.map((c) =>
    renderCategory(c, categoryTotals[c.id])
  );

  return (
    <div>
      <h1>
        {name} = {formatCurrency(ownerSummary.totalAmount)}
      </h1>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th className="text-start">Account</th>
            <th className="text-end">Variance</th>
            <th className="text-end">Portion</th>
          </tr>
        </thead>
        <tbody>{accountViews}</tbody>
      </Table>

      <Tab.Container>
        <Row>
          <Col>
            <Nav variant="pills" className="flex-column">
              {categoryTabs}
            </Nav>
          </Col>
          <Col sm={9}>
            <Tab.Content>{categoryPages}</Tab.Content>
          </Col>
        </Row>
      </Tab.Container>
    </div>
  );
}

function renderAccount(
  owner: Wenab.Owner,
  snapshot: Wenab.SnapshotDto,
  account: Wenab.AccountDto,
  transactions: Wenab.TransactionSummaryDto[]
): JSX.Element {
  const accountTransactions = transactions.filter(
    (t) => t.account == account.id
  );

  let total = 0;
  let owe = 0;

  accountTransactions.forEach((t) => {
    total += t.ownerAmount;
    const backingAccount = snapshot.accounts.find((a) => {
      console.log(a.id + " vs " + t.account);
      return a.id == t.account;
    })?.backingAccount;
    const isOwner =
      snapshot.accounts.find((a) => {
        console.log(a.id + " vs " + t.account);
        return a.id == backingAccount;
      })?.owner == owner;

    if (!isOwner) {
      owe += t.ownerAmount;
    }
  });

  return (
    <tr>
      <td className="text-start">{account.name}</td>
      <td className="text-end">{formatCurrency(total)}</td>
      <td className="text-end">{formatCurrency(owe)}</td>
    </tr>
  );
}

function renderCategory(
  category: Wenab.CategoryDto,
  amount: number
): JSX.Element {
  return (
    <Nav.Item>
      <Nav.Link
        eventKey={category.id}
        className="d-flex justify-content-between"
      >
        <span>{category.name}</span>
        <span>{formatCurrency(amount)}</span>
      </Nav.Link>
    </Nav.Item>
  );
}

function renderTransaction(
  transaction: Wenab.TransactionSummaryDto
): JSX.Element {
  return (
    <tr>
      <td className="text-start">{transaction.payee}</td>
      <td className="text-start">{transaction.memo}</td>
      <td className="text-end">{formatCurrency(transaction.ownerAmount)}</td>
    </tr>
  );
}

function formatCurrency(value: number): string {
  if (value == null || value == 0) {
    return "$0.000";
  }

  return `${value < 0 ? "-" : ""}$${Math.abs(value / 1000).toLocaleString(
    undefined,
    { minimumFractionDigits: 3 }
  )}`;
}

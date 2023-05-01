import { Owner } from "models";
import { GetOwner } from "models/Owners";
import {
  Container,
  Navbar,
  ToggleButton,
  ToggleButtonGroup,
} from "react-bootstrap";

type Props = {
  onOwnerChange: (owner: Owner) => void;
  currentOwner: Owner;
};

export function Toolbar(props: Props): JSX.Element {
  const { onOwnerChange, currentOwner } = props;

  const owners = [GetOwner(1), GetOwner(2)];
  const ownerButtons = owners.map((o) => renderOwnerButton(o, onOwnerChange));

  return (
    <Navbar bg="dark">
      <Container>
        <ToggleButtonGroup name="owners" type="radio" value={currentOwner.name}>
          {ownerButtons}
        </ToggleButtonGroup>
      </Container>
    </Navbar>
  );
}

function renderOwnerButton(
  owner: Owner,
  onOwnerChange: (owner: Owner) => void
): JSX.Element {
  return (
    <ToggleButton
      className="owner-button"
      key={owner.name}
      onClick={() => onOwnerChange(owner)}
      value={owner.name}
      variant="outline-primary"
    >
      {owner.name}
    </ToggleButton>
  );
}

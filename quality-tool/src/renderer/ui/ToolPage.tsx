import {Alignment, Button, Card, Elevation, Navbar} from '@blueprintjs/core';
import kivaProtocol from '../../../assets/kivaProtocol.svg';
import './ToolPage.scss';

const ToolPage = () => {
  return (
    <div className="ToolPageContainer">
      <Navbar>
        <Navbar.Group align={Alignment.LEFT}>
          <Navbar.Heading>
            Fingerprint Quality Tool
          </Navbar.Heading>
          <Navbar.Divider />
          <Button className="bp3-minimal" icon="settings" text="Settings"/>
        </Navbar.Group>
        <Navbar.Group align={Alignment.RIGHT}>
          <Navbar.Heading><img width="100px" alt="icon" src={kivaProtocol}/></Navbar.Heading>
        </Navbar.Group>
      </Navbar>
      <div className="ToolPageContainer_Body">
        <Card interactive={true} elevation={Elevation.ZERO}>
          <h5>Control Fingerprint</h5>
          <div className="FingerprintPanel">

          </div>
          <Button>Upload</Button>
        </Card>
        <Card interactive={true} elevation={Elevation.TWO}>
          <h5>Test Fingerprint</h5>
          <div className="FingerprintPanel">

          </div>
          <Button>Scan</Button>
        </Card>
      </div>
    </div>
  );
};

export default ToolPage;

import { Alignment, Button, Card, Elevation, Navbar } from '@blueprintjs/core';
import kivaProtocol from '../../../assets/kivaProtocol.svg';
import './ToolPage.scss';
import { useState } from 'react';

const ToolPage = () => {
  const [controlFingerprint, setControlFingerprint] = useState('');
  const uploadControlFingerprint = () => {
    // opens a window to choose file
  };

  const scanTestFingerprint = () => {};

  return (
    <div className="ToolPageContainer">
      <Navbar>
        <Navbar.Group align={Alignment.LEFT}>
          <Navbar.Heading>Fingerprint Quality Tool</Navbar.Heading>
          <Navbar.Divider />
          <Button className="bp3-minimal" icon="settings" text="Settings" />
        </Navbar.Group>
        <Navbar.Group align={Alignment.RIGHT}>
          <Navbar.Heading>
            <img width="100px" alt="icon" src={kivaProtocol} />
          </Navbar.Heading>
        </Navbar.Group>
      </Navbar>
      <div className="ToolPageContainer_Body">
        <Card elevation={Elevation.ZERO}>
          <h5>Control Fingerprint</h5>
          <div className="FingerprintPanel">
            <img src={controlFingerprint} />
          </div>
          <Button onClick={uploadControlFingerprint}>Upload</Button>
        </Card>
        <Card elevation={Elevation.TWO}>
          <h5>Test Fingerprint</h5>
          <div className="FingerprintPanel" />
          <Button onClick={scanTestFingerprint}>Scan</Button>
        </Card>
      </div>
    </div>
  );
};

export default ToolPage;

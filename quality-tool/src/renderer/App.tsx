import { MemoryRouter as Router, Routes, Route } from 'react-router-dom';
import ToolPage from './ui/ToolPage';
import './App.css';


export default function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<ToolPage />} />
      </Routes>
    </Router>
  );
}

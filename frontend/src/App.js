import './App.css';
import '@fontsource/roboto';
import * as React from 'react';
import CssBaseline from '@mui/material/CssBaseline';
import Navigation from './components/Navigation/Navigation';
import Router from './components/Router/Router';
function App() {
  const token = localStorage.getItem('token');

  return (
    <div className="App">
      {token && <Navigation />}
      <CssBaseline />
      <Router />
    </div>
  );
}

export default App;

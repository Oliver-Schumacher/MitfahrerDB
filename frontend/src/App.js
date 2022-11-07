import './App.css';
import '@fontsource/roboto';
import CssBaseline from '@mui/material/CssBaseline';
import { Routes, Route } from 'react-router-dom';
import Error404 from './pages/Error/Error404';
import SignUp from './pages/Auth/SignUp';
import SignIn from './pages/Auth/SignIn';
import ResponsiveAppBar from './components/Navigation/Navigation';

function App() {
  return (
    <div className="App">
      <CssBaseline />
      <ResponsiveAppBar />
      <Routes>
        <Route index element={<SignIn />} />
        <Route path="SignUp" element={<SignUp />} />
        <Route path="SignIn" element={<SignIn />} />
        <Route path="/home" element={<Home />} />
        <Route path="*" element={<Error404 />} />
      </Routes>
    </div>
  );
}

const Home = () => {
  return <p>There is nothing here yet but this will be the Homepage soon!</p>;
};

export default App;

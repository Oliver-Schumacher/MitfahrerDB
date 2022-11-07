import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Error404 from '../../pages/Error/index';
import Register from '../../pages/Auth/Register/index';
import Login from '../../pages/Auth/Login/index';
import Profile from '../../pages/Profile/index';
import Search from '../../pages/Search/index';
import Manage from '../../pages/Manage/index';
import RouteGuard from './RouteGuard';

function Router() {
  return (
    <Routes>
      <Route index element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/login" element={<Login />} />
      <Route path="/search" element={<Search />} />
      <Route path="/manage" element={<Manage />} />
      <Route path="/profile" element={<Profile />} />
      <Route path="*" element={<Error404 />} />
    </Routes>
  );
}

export default Router;

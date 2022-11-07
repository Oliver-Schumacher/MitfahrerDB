import React from 'react';
import { Navigate } from 'react-router-dom';

function RouteGuard({ children }) {
  function hasJWT() {
    let flag = false;

    //check user has JWT token
    localStorage.getItem('token') ? (flag = true) : (flag = false);

    return flag;
  }

  if (!hasJWT()) {
    <Navigate to="/login" replace />;
  }

  return children;
}

export default RouteGuard;

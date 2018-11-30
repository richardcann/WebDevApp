import React from 'react';
import { Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './components/Home';
import ErrorBoundary from './components/ErrorBoundary';
import Login from './components/Login';
import Register from './components/Register';
import PrivateRoute from './components/PrivateRoute';
import LoadingIndicator from './components/LoadingIndicator';

export default () => (
  <ErrorBoundary>
    <Layout>
      <PrivateRoute exact path="/" component={Home} />
      <Route
        path="/loginpage"
        component={() => {
          window.location = `${window.location.origin}/loginpage`;
          console.log(window.location);
          return <LoadingIndicator />;
        }}
      />
      <Route path="/home" component={Home} />
      <Route path="/login" component={Login} />
      <Route path="/register" component={Register} />
    </Layout>
  </ErrorBoundary>
);

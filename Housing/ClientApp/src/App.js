import React from 'react';
import { Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import ErrorBoundary from './components/ErrorBoundary';
import Login from './components/Login';
import { PrivateRoute } from './components/PrivateRoute';

export default () => (
  <ErrorBoundary>
    <Layout>
      <PrivateRoute exact path="/" component={Home} />
      <Route path="/counter" component={Counter} />
      <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
      <Route path="/login" component={Login} />
    </Layout>
  </ErrorBoundary>
);

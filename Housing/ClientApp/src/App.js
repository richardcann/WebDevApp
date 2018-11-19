import React from 'react';
import { Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './components/Home';
import LandlordHome from './components/LandlordHome';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import ErrorBoundary from './components/ErrorBoundary';
import Login from './components/Login';
import Register from './components/Register';
import MapCard from './components/MapCard';
import Form from './components/PropertyForm';
import { PrivateRoute } from './components/PrivateRoute';

export default () => (
  <ErrorBoundary>
    <Layout>
      <PrivateRoute exact path="/" component={Home} />
      <Route path="/counter" component={Counter} />
      <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
      <Route path="/login" component={Login} />
      <Route path="/register" component={Register} />
      <Route path="/map" component={MapCard} />
      <Route path="/landlord" component={LandlordHome} />
      <Route path="/form" component={Form} />
    </Layout>
  </ErrorBoundary>
);

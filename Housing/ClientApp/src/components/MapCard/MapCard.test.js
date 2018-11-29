import React from 'react';
import MapCard from './';
import ReactDOM from 'react-dom';
import ErrorBoundary from '../ErrorBoundary';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <ErrorBoundary>
      <MapCard id={'1'} position={[0, 0]} />
    </ErrorBoundary>,
    div
  );
});

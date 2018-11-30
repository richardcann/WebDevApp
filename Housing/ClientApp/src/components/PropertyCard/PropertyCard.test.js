import React from 'react';
import PropertyCard from './';
import { exampleProperty } from '../sampleConstants';
import ReactDOM from 'react-dom';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <PropertyCard
      id={'1'}
      extra={<div>test</div>}
      property={exampleProperty}
    />,
    div
  );
});

import React from 'react';
import PropertyForm from './';
import { exampleProperty } from '../sampleConstants';
import ReactDOM from 'react-dom';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <PropertyForm
      visible={true}
      onCancel={() => {}}
      property={exampleProperty}
    />,
    div
  );
});

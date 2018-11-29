import React from 'react';
import PropertyForm from './';
import { exampleProperty } from '../sampleConstants';
import { shallow } from 'enzyme';

it('renders without crashing', () => {
  const renderedComponent = shallow(
    <PropertyForm
      visible={true}
      onCancel={() => {}}
      property={exampleProperty}
    />
  );
  expect(renderedComponent.length).toEqual(1);
});

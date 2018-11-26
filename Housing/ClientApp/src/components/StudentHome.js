import React from 'react';
import { connect } from 'react-redux';
import { exampleProperty } from './sampleConstants';
import PropertyCard from './PropertyCard';

function StudentHome(props) {
  return (
    <div>
      <PropertyCard id={'1'} property={exampleProperty} />
      <PropertyCard id={'2'} property={exampleProperty} />
      <PropertyCard id={'3'} property={exampleProperty} />
      <PropertyCard id={'4'} property={exampleProperty} />
    </div>
  );
}

export default connect()(StudentHome);

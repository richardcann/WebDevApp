import React from 'react';
import { connect } from 'react-redux';
import PropertyCard from './PropertyCard';
import { exampleProperty } from './sampleConstants';

const Home = props => (
  <div>
    <PropertyCard id={'1'} property={exampleProperty} />
    <PropertyCard id={'2'} property={exampleProperty} />
    <PropertyCard id={'3'} property={exampleProperty} />
    <PropertyCard id={'4'} property={exampleProperty} />
  </div>
);

export default connect()(Home);

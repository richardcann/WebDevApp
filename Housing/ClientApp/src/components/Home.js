import React from 'react';
import { connect } from 'react-redux';
import PropertyCard from './PropertyCard';

const Home = props => (
  <div>
    <PropertyCard id={'1'} />
    <PropertyCard id={'2'} />
    <PropertyCard id={'3'} />
    <PropertyCard id={'4'} />
  </div>
);

export default connect()(Home);

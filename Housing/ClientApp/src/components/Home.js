import React from 'react';
import { connect } from 'react-redux';
import PropertyCard from './PropertyCard';

const Home = props => <PropertyCard />;

export default connect()(Home);

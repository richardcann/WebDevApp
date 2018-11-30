import 'antd/lib/alert/style/css';
import React from 'react';
import { connect } from 'react-redux';
import PropertyCard from './PropertyCard';
import { studentActions } from '../store/actions';
import LoadingIndicator from './LoadingIndicator';
import { Alert } from 'antd';

function StudentHome(props) {
  const { properties, getProperties } = props;

  if (typeof properties === 'undefined') {
    getProperties();
  }
  return (
    <div>
      {typeof properties !== 'undefined' && properties !== null ? (
        properties.map(property => {
          return (
            <PropertyCard id={property.id.toString()} property={property} />
          );
        })
      ) : (
        <LoadingIndicator />
      )}
      {properties && properties.length === 0 ? (
        <Alert
          message="No Properties"
          description="There are no properties to display."
          type="info"
        />
      ) : null}
    </div>
  );
}

function mapStateToProps(state) {
  const { properties } = state.student;
  return {
    properties
  };
}

const mapDispatchToProps = dispatch => {
  return {
    getProperties: () => {
      dispatch(studentActions.getApprovedProperties());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(StudentHome);

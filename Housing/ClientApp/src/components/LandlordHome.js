import 'antd/lib/tag/style/css';
import 'antd/lib/button/style/css';
import React from 'react';
import { connect } from 'react-redux';
import PropertyCard from './PropertyCard';
import PropertyForm from './PropertyForm';
import MessageModal from './MessageModal';
import {
  exampleProperty,
  disapprovedProperty,
  pendingProperty
} from './sampleConstants';
import { Tag, Button } from 'antd';
import { landlordActions } from '../store/actions';

function LandlordHome(props) {
  const {
    addingProperty,
    editingProperty,
    addNewProperty,
    propertyAdded,
    editProperty,
    submitProperty,
    currentProperties,
    setProperties,
    showDisapproved,
    showCurrentDisapproved,
    hideDisapproved,
    submitNewProperty,
    getProperties
  } = props;

  if (typeof currentProperties === 'undefined') {
    getProperties();
  }

  const handleClick = property => {
    editProperty(property);
  };

  const onCancel = () => {
    propertyAdded();
  };

  const submitEdit = values => {
    currentProperties[editingProperty] = {
      ...currentProperties[editingProperty],
      ...values,
      propertyStatus: 0
    };
    setProperties(currentProperties);
    submitProperty(currentProperties[editingProperty]);
  };

  const handleNewProperty = values => {
    const requestOptions = {
      method: 'GET'
    };

    fetch(
      `https://nominatim.openstreetmap.org/search?format=json&q=${
        values.address
      }`,
      requestOptions
    ).then(response => {
      response.json().then(data => {
        const lat = data[0].lat;
        const lon = data[0].lon;
        values = { ...values, latitude: lat, longitude: lon };
        const newProperty = { ...values, propertyStatus: 0 };
        currentProperties.push(newProperty);
        console.log(currentProperties);
        setProperties(currentProperties);
        submitNewProperty(newProperty);
      });
    });
  };

  const style = {
    display: 'flex',
    justifyContent: 'space-between',
    width: '10em'
  };

  return (
    <div>
      <Button
        type="primary"
        icon="plus-circle"
        loading={false}
        onClick={() => {
          addNewProperty();
        }}
      >
        Add Property
      </Button>
      <PropertyForm
        visible={addingProperty}
        onCancel={onCancel}
        onSubmit={handleNewProperty}
      />
      <PropertyForm
        visible={editingProperty !== null && editingProperty >= 0}
        property={currentProperties ? currentProperties[editingProperty] : null}
        onCancel={submitProperty}
        onSubmit={submitEdit}
      />
      {typeof showDisapproved !== 'undefined' && showDisapproved !== null ? (
        <MessageModal
          visible={showDisapproved !== null}
          onCancel={hideDisapproved}
          noEdit={true}
          title={'Rejection Reason: '}
          message={currentProperties[showDisapproved].rejections[0].description}
        />
      ) : null}
      {currentProperties
        ? currentProperties.map((current, index) => {
            const color =
              current.propertyStatus === 0 || current.propertyStatus === 2
                ? current.propertyStatus === 1
                  ? 'red'
                  : 'orange'
                : 'green';
            return (
              <PropertyCard
                id={index.toString()}
                property={current}
                extra={
                  <div style={style}>
                    <Tag
                      onClick={() => {
                        color === 'red' ? showCurrentDisapproved(index) : null;
                      }}
                      color={color}
                    >
                      {current.propertyStatus === 0
                        ? 'pending'
                        : current.propertyStatus === 2
                          ? 'rejected'
                          : 'approved'}
                    </Tag>
                    <a onClick={() => handleClick(index)}>Edit</a>
                  </div>
                }
              />
            );
          })
        : null}
    </div>
  );
}
function mapStateToProps(state) {
  const {
    addingProperty,
    editingProperty,
    currentProperties,
    showDisapproved
  } = state.landlord;
  return {
    addingProperty,
    editingProperty,
    currentProperties,
    showDisapproved
  };
}

const mapDispatchToProps = dispatch => {
  return {
    addNewProperty: () => {
      dispatch(landlordActions.addNewProperty());
    },
    propertyAdded: () => {
      dispatch(landlordActions.propertyAdded());
    },
    editProperty: property => {
      dispatch(landlordActions.editProperty(property));
    },
    submitEditedProperty: property => {
      dispatch(landlordActions.submitEditedProperty(property));
    },
    submitNewProperty: property => {
      dispatch(landlordActions.submitNewProperty(property));
    },
    setProperties: properties => {
      dispatch(landlordActions.setProperties(properties));
    },
    getProperties: () => {
      dispatch(landlordActions.getProperties());
    },
    showCurrentDisapproved: index => {
      dispatch(landlordActions.showCurrentDisapproved(index));
    },
    hideDisapproved: () => {
      dispatch(landlordActions.hideDisapproved());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(LandlordHome);

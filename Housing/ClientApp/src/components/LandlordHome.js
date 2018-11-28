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
    submitEditedProperty,
    currentProperties,
    setProperties,
    showDisapproved,
    showCurrentDisapproved,
    hideDisapproved,
    submitNewProperty,
    getProperties,
    cancelModal
  } = props;

  if (typeof currentProperties === 'undefined') {
    getProperties();
  }

  const handleClick = property => {
    editProperty(property);
  };

  const onCancel = () => {
    cancelModal();
  };

  const submitEdit = values => {
    values.images.map(url => {
      currentProperties[editingProperty].images.push(url);
    });
    const images = currentProperties[editingProperty].images;
    currentProperties[editingProperty] = {
      ...currentProperties[editingProperty],
      ...values,
      images,
      propertyStatus: 1
    };
    setProperties(currentProperties);
    submitEditedProperty(currentProperties[editingProperty]);
  };

  const handleNewProperty = values => {
    const requestOptions = {
      method: 'GET'
    };

    const addressQuery = `${values.addressLine1} ${
      values.postcode ? values.postcode : values.city ? values.city : ''
    }`;

    fetch(
      `https://nominatim.openstreetmap.org/search?format=json&q=${addressQuery}`,
      requestOptions
    ).then(response => {
      response.json().then(data => {
        const lat = data[0].lat;
        const lon = data[0].lon;
        values = { ...values, latitude: lat, longitude: lon };
        const newProperty = { ...values, propertyStatus: 1 };
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
      {addingProperty ? (
        <PropertyForm
          visible={addingProperty}
          onCancel={onCancel}
          onSubmit={handleNewProperty}
        />
      ) : null}
      {editingProperty !== null && editingProperty >= 0 ? (
        <PropertyForm
          visible={editingProperty !== null && editingProperty >= 0}
          property={
            currentProperties ? currentProperties[editingProperty] : null
          }
          onCancel={cancelModal}
          onSubmit={submitEdit}
        />
      ) : null}
      {typeof showDisapproved !== 'undefined' && showDisapproved !== null ? (
        <MessageModal
          visible={showDisapproved !== null}
          onCancel={hideDisapproved}
          noEdit={true}
          title={'Rejection Reason: '}
          message={currentProperties[showDisapproved].rejections}
        />
      ) : null}
      {currentProperties
        ? currentProperties.map((current, index) => {
            const color =
              current.propertyStatus === 1 || current.propertyStatus === 2
                ? current.propertyStatus === 2
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
                      {current.propertyStatus === 1
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
    },
    cancelModal: () => {
      dispatch(landlordActions.cancelModal());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(LandlordHome);

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
    hideDisapproved
  } = props;

  if (typeof currentProperties === 'undefined') {
    setProperties([exampleProperty, disapprovedProperty, pendingProperty]);
  }
  /*const requestOptions = {
    method: 'GET'
  };

  fetch('https://nominatim.openstreetmap.org/search?format=json&q=6+University+Road+Southampton', requestOptions).then((response) => {
    response.json().then((data) => console.log(data));
  });*/

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
      status: 'pending'
    };
    setProperties(currentProperties);
    submitProperty();
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
        const position = [lat, lon];
        values = { ...values, position };
        currentProperties.push({ ...values, status: 'pending' });
        console.log(currentProperties);
        setProperties(currentProperties);
        propertyAdded();
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
          title={currentProperties[showDisapproved].message.author}
          message={currentProperties[showDisapproved].message.description}
        />
      ) : null}
      {currentProperties
        ? currentProperties.map((current, index) => {
            const color =
              current.status === 'approved' || current.status === 'disapproved'
                ? current.status === 'approved'
                  ? 'green'
                  : 'red'
                : 'orange';
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
                      {current.status}
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
    submitProperty: () => {
      dispatch(landlordActions.submitProperty());
    },
    setProperties: properties => {
      dispatch(landlordActions.setProperties(properties));
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

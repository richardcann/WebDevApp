import 'antd/lib/icon/style/css';
import 'antd/lib/alert/style/css';
import React from 'react';
import { connect } from 'react-redux';
import PropertyCard from './PropertyCard';
import MessageModal from './MessageModal';
import { pendingProperty } from './sampleConstants';
import { officerActions } from '../store/actions';
import LoadingIndicator from './LoadingIndicator';
import { Icon, Alert } from 'antd';

function OfficerHome(props) {
  const {
    currentProperties,
    setProperties,
    disapprovedProperty,
    disapproveProperty,
    getProperties,
    submitDisapproval,
    approveCurrentProperty,
    closeModal
  } = props;

  if (typeof currentProperties === 'undefined') {
    getProperties();
  }

  const onCancelModal = () => {
    closeModal();
  };

  const submitMessage = message => {
    currentProperties[disapprovedProperty] = {
      ...currentProperties[disapprovedProperty],
      propertyStatus: 2
    };
    setProperties(currentProperties);
    submitDisapproval(currentProperties[disapprovedProperty].id, message);
  };

  const approveProperty = index => {
    currentProperties[index] = {
      ...currentProperties[index],
      propertyStatus: 0
    };
    const propId = currentProperties[index].id;
    currentProperties.splice(index, 1);
    setProperties(currentProperties);
    approveCurrentProperty(propId);
  };

  return (
    <div>
      {currentProperties ? (
        <div>
          {disapprovedProperty !== null &&
          typeof disapprovedProperty !== 'undefined' ? (
            <MessageModal
              visible={true}
              noEdit={false}
              onSubmit={submitMessage}
              onCancel={onCancelModal}
              title={'Reason for disapproval:'}
              message={
                currentProperties[disapprovedProperty].message
                  ? currentProperties[disapprovedProperty].message.description
                  : null
              }
            />
          ) : null}
          {currentProperties
            ? currentProperties.map((current, index) => {
                return (
                  <PropertyCard
                    id={index.toString()}
                    property={current}
                    extra={
                      <div>
                        <Icon
                          style={{ fontSize: '2em', cursor: 'pointer' }}
                          onClick={() => approveProperty(index)}
                          type="check-circle"
                          theme="twoTone"
                          twoToneColor="#52c41a"
                        />
                        <Icon
                          style={{ fontSize: '2em', cursor: 'pointer' }}
                          onClick={() => disapproveProperty(index)}
                          type="close-circle"
                          theme="twoTone"
                          twoToneColor="#ff0000"
                        />
                      </div>
                    }
                  />
                );
              })
            : null}
          {currentProperties && currentProperties.length === 0 ? (
            <Alert
              message="No Properties"
              description="There are no properties to display."
              type="info"
            />
          ) : null}
        </div>
      ) : (
        <LoadingIndicator />
      )}
    </div>
  );
}
function mapStateToProps(state) {
  const { currentProperties, disapprovedProperty } = state.officer;
  return {
    currentProperties,
    disapprovedProperty
  };
}

const mapDispatchToProps = dispatch => {
  return {
    setProperties: properties => {
      dispatch(officerActions.setProperties(properties));
    },
    disapproveProperty: index => {
      dispatch(officerActions.disapproveProperty(index));
    },
    approveCurrentProperty: index => {
      dispatch(officerActions.approveProperty(index));
    },
    submitDisapproval: (index, description) => {
      dispatch(officerActions.submitDisapproval(index, description));
    },
    getProperties: () => {
      dispatch(officerActions.getProperties());
    },
    closeModal: () => {
      dispatch(officerActions.closeModal());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(OfficerHome);

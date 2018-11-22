import 'antd/lib/icon/style/css';
import React from 'react';
import { connect } from 'react-redux';
import PropertyCard from './PropertyCard';
import MessageModal from './MessageModal';
import { pendingProperty } from './sampleConstants';
import { officerActions } from '../store/actions';
import { Icon } from 'antd';

function OfficerHome(props) {
  const {
    currentProperties,
    setProperties,
    disapprovedProperty,
    disapproveProperty,
    submitDisapproval
  } = props;

  if (typeof currentProperties === 'undefined') {
    setProperties([pendingProperty, pendingProperty, pendingProperty]);
  }

  const onCancelModal = () => {
    submitDisapproval();
  };

  const submitMessage = message => {
    currentProperties[disapprovedProperty] = {
      ...currentProperties[disapprovedProperty],
      message: {
        author: 'Current User',
        description: message
      },
      status: 'disapproved'
    };
    setProperties(currentProperties);
    submitDisapproval();
  };

  const approveProperty = index => {
    currentProperties[index] = {
      ...currentProperties[index],
      status: 'approved'
    };
    setProperties(currentProperties);
  };

  return (
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
    submitDisapproval: () => {
      dispatch(officerActions.submitDisapproval());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(OfficerHome);

import { userHelper } from '../helpers/userHelper';

export function setProperties(properties) {
  return { type: 'SET_PROPERTIES', properties };
}

export function disapproveProperty(index) {
  return { type: 'DISAPPROVE_PROPERTY', index };
}

export function submitDisapproval(index, description) {
  return dispatch => {
    userHelper.submitRejection(index, description).then(
      () => {
        //history.push('/login');
        dispatch({ type: 'SUBMIT_DISAPPROVAL' });
        //dispatch(alertActions.success('Registration successful'));
      },
      error => {
        console.log(error);
      }
    );
  };
}

export function closeModal() {
  return { type: 'SUBMIT_DISAPPROVAL' };
}

export function approveProperty(index) {
  return dispatch => {
    userHelper.approveProperty(index).then(
      () => {
        //history.push('/login');
        dispatch(closeModal());
        //dispatch(alertActions.success('Registration successful'));
      },
      error => {
        console.log(error);
      }
    );
  };
}

export function getProperties() {
  return dispatch => {
    userHelper.getOfficerProps().then(
      properties => {
        //history.push('/login');
        dispatch(setProperties(properties));
        //dispatch(alertActions.success('Registration successful'));
      },
      error => {
        console.log(error);
      }
    );
  };
}

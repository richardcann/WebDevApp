import { userHelper } from '../helpers/userHelper';

export function addNewProperty() {
  return { type: 'ADD_NEW_PROPERTY' };
}

export function propertyAdded() {
  return { type: 'PROPERTY_ADDED' };
}

export function editProperty(property) {
  return { type: 'EDIT_PROPERTY', property };
}

export function submitEditedProperty(property) {
  return dispatch => {
    userHelper.submitEdit(property).then(
      () => {
        //history.push('/login');
        dispatch({ type: 'SUBMIT_PROPERTY' });
        //dispatch(alertActions.success('Registration successful'));
      },
      error => {
        console.log(error);
      }
    );
  };
}

export function submitNewProperty(property) {
  return dispatch => {
    userHelper.submitNew(property).then(
      () => {
        //history.push('/login');
        dispatch(propertyAdded());
        //dispatch(alertActions.success('Registration successful'));
      },
      error => {
        console.log(error);
      }
    );
  };
}

export function showCurrentDisapproved(index) {
  return { type: 'SHOW_DISAPPROVED', index };
}

export function hideDisapproved() {
  return { type: 'HIDE_DISAPPROVED' };
}

export function setProperties(properties) {
  return { type: 'SET_PROPERTIES', properties };
}

export function getProperties() {
  return dispatch => {
    userHelper.getLandlordProps().then(
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

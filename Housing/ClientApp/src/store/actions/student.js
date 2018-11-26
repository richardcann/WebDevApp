import { history } from '../helpers/history';
import { userHelper } from '../helpers/userHelper';

export function getApprovedProperties() {
  return dispatch => {
    userHelper.getApprovedProperties().then(
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

  function setProperties(properties) {
    return { type: 'SET_PROPERTIES', properties };
  }
}

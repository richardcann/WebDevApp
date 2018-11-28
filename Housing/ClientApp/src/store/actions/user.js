import { userHelper } from '../helpers/userHelper';
import { history } from '../helpers/history';

export const userActions = {
  getAll,
  delete: _delete
};

export function login(username, password) {
  return dispatch => {
    dispatch(request({ username }));

    userHelper.login(username, password).then(
      user => {
        dispatch(success(user));
        history.push('/');
      },
      error => {
        dispatch(failure(error.toString()));
        //dispatch(alertActions.error(error.toString()));
      }
    );
  };

  function request(user) {
    return { type: 'LOGIN_REQUEST', user };
  }
  function success(user) {
    return { type: 'LOGIN_SUCCESS', user };
  }
  function failure(error) {
    return { type: 'LOGIN_FAILURE', error };
  }
}

export function setUser(token) {
  return dispatch => {
    dispatch(request(token));

    userHelper.getUser(token).then(
      user => {
        localStorage.setItem('user', JSON.stringify({ ...user, token }));
        dispatch(success({ ...user, token }));
        //history.push('/');
      },
      error => {
        dispatch(failure(error.toString()));
        //dispatch(alertActions.error(error.toString()));
      }
    );
  };

  function request(user) {
    return { type: 'LOGIN_REQUEST', user };
  }
  function success(user) {
    return { type: 'LOGIN_SUCCESS', user };
  }
  function failure(error) {
    return { type: 'LOGIN_FAILURE', error };
  }
}

export function logout() {
  userHelper.logout();
  return { type: 'LOGOUT' };
}

export function register(user) {
  return dispatch => {
    dispatch(request(user));

    userHelper.register(user).then(
      user => {
        dispatch(success(user));
        history.push('/login');
        console.log('Registration successful');
        //dispatch(alertActions.success('Registration successful'));
      },
      error => {
        dispatch(failure(error.toString()));
        //dispatch(alertActions.error(error.toString()));
      }
    );
  };

  function request(user) {
    return { type: 'REGISTER_REQUEST', user };
  }
  function success(user) {
    return { type: 'REGISTER_SUCCESS', user };
  }
  function failure(error) {
    return { type: 'REGISTER_FAILURE', error };
  }
}

function getAll() {
  return dispatch => {
    dispatch(request());

    userHelper
      .getAll()
      .then(
        users => dispatch(success(users)),
        error => dispatch(failure(error.toString()))
      );
  };

  function request() {
    return { type: 'GETALL_REQUEST' };
  }
  function success(users) {
    return { type: 'GETALL_SUCCESS', users };
  }
  function failure(error) {
    return { type: 'GETALL_FAILURE', error };
  }
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
  return dispatch => {
    dispatch(request(id));

    userHelper
      .delete(id)
      .then(
        user => dispatch(success(id)),
        error => dispatch(failure(id, error.toString()))
      );
  };

  function request(id) {
    return { type: 'DELETE_REQUEST', id };
  }
  function success(id) {
    return { type: 'DELETE_SUCCESS', id };
  }
  function failure(id, error) {
    return { type: 'DELETE_FAILURE', id, error };
  }
}

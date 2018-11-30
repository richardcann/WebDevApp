import Cookies from 'universal-cookie';
export const userHelper = {
  login,
  logout,
  register,
  getAll,
  getApprovedProperties,
  getLandlordProps,
  submitEdit,
  submitNew,
  getOfficerProps,
  submitRejection,
  approveProperty,
  getUser,
  getById,
  update,
  delete: _delete
};

function authHeader() {
  // return authorization header with jwt token
  let user = JSON.parse(localStorage.getItem('user'));

  if (user && user.token) {
    return {
      Authorization: 'Bearer ' + user.token,
      'Content-Type': 'application/json'
    };
  } else {
    return {};
  }
}

function getApprovedProperties() {
  const requestOptions = {
    method: 'GET',
    headers: authHeader()
  };

  return fetch(`api/properties/approved`, requestOptions).then(handleResponse);
}

function getLandlordProps() {
  const requestOptions = {
    method: 'GET',
    headers: authHeader()
  };

  return fetch(`api/properties/myproperties`, requestOptions).then(
    handleResponse
  );
}

function getOfficerProps() {
  const requestOptions = {
    method: 'GET',
    headers: authHeader()
  };

  return fetch(`api/properties/pending`, requestOptions).then(handleResponse);
}

function getUser(token) {
  const requestOptions = {
    method: 'GET',
    headers: {
      Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    }
  };

  return fetch(`api/users/fromtoken`, requestOptions).then(handleResponse);
}

function submitRejection(index, message) {
  const requestOptions = {
    method: 'POST',
    headers: authHeader(),
    body: JSON.stringify({ description: message })
  };

  return fetch(`api/rejections/add/${index}`, requestOptions).then(
    handleResponse
  );
}

function approveProperty(index) {
  const requestOptions = {
    method: 'POST',
    headers: authHeader()
  };

  return fetch(`api/properties/approve/${index}`, requestOptions).then(
    handleResponse
  );
}

function submitEdit(property) {
  const requestOptions = {
    method: 'POST',
    headers: authHeader(),
    body: JSON.stringify(property)
  };

  return fetch(`api/properties/edit/${property.id}`, requestOptions).then(
    handleResponse
  );
}

function submitNew(property) {
  const requestOptions = {
    method: 'POST',
    headers: authHeader(),
    body: JSON.stringify(property)
  };

  return fetch(`api/properties/add`, requestOptions).then(handleResponse);
}

function login(username, password) {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password })
  };

  return fetch(`api/users/authenticate`, requestOptions)
    .then(handleResponse)
    .then(user => {
      // login successful if there's a jwt token in the response
      if (user.token) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(user));
      }

      return user;
    });
}

function logout() {
  // remove user from local storage to log user out
  localStorage.removeItem('user');
  //const cookies = document.cookie.split('Token=')[0];
  //console.log(cookies);
  const cookie = new Cookies();
  cookie.remove('Token');
  //document.cookie = `${cookies}`;
}

function getAll() {
  const requestOptions = {
    method: 'GET',
    headers: authHeader()
  };

  return fetch(`users`, requestOptions).then(handleResponse);
}

function getById(id) {
  const requestOptions = {
    method: 'GET',
    headers: authHeader()
  };

  return fetch(`users/${id}`, requestOptions).then(handleResponse);
}

function register(user) {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(user)
  };

  return fetch(`api/users/register`, requestOptions).then(handleResponse);
}

function update(user) {
  const requestOptions = {
    method: 'PUT',
    headers: { ...authHeader(), 'Content-Type': 'application/json' },
    body: JSON.stringify(user)
  };

  return fetch(`users/${user.id}`, requestOptions).then(handleResponse);
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
  const requestOptions = {
    method: 'DELETE',
    headers: authHeader()
  };

  return fetch(`users/${id}`, requestOptions).then(handleResponse);
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        window.location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}

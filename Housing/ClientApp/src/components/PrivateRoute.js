import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import Home from './Home';

/*export const PrivateRoute = ({ component: Component, ...rest }) => (
  <Route
    {...rest}
    render={props =>
      localStorage.getItem('user') ? (
        <Component {...props} />
      ) : (
        <Redirect
          to={{ pathname: '/login', state: { from: props.location } }}
        />
      )
    }
  />
);*/

function PrivateRoute(props) {
  const { component: Component, rest, loggedIn, loggingIn, cookie } = props;
  console.log(typeof cookie !== 'undefined' && cookie && cookie.length > 0);
  console.log(loggedIn);
  console.log(typeof loggingIn !== 'undefined' && loggingIn);
  console.log(
    (typeof cookie !== 'undefined' && cookie && cookie.length > 0) ||
      (loggedIn || (typeof loggingIn !== 'undefined' && loggingIn))
  );
  if (
    (typeof cookie !== 'undefined' && cookie && cookie.length > 0) ||
    (loggedIn || (typeof loggingIn !== 'undefined' && loggingIn))
  ) {
    console.log('truth');
  } else {
    console.log('no');
  }
  return (
    <Route
      {...rest}
      render={props =>
        (typeof cookie !== 'undefined' && cookie && cookie.length > 0) ||
        (loggedIn || (typeof loggingIn !== 'undefined' && loggingIn)) ? (
          <Redirect
            to={{ pathname: '/home', state: { from: props.location } }}
          />
        ) : (
          <Redirect
            to={{ pathname: '/loginpage', state: { from: props.location } }}
          />
        )
      }
    />
  );
}

function mapStateToProps(state) {
  return {
    loggedIn: !!state.users.user,
    loggingIn: state.users.loggingIn,
    cookie: state.users.cookie
  };
}

export default connect(mapStateToProps)(PrivateRoute);

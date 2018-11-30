import React from 'react';
import { connect } from 'react-redux';
import StudentHome from './StudentHome';
import LandlordHome from './LandlordHome';
import OfficerHome from './OfficerHome';
import { userActions } from '../store/actions';

function Home(props) {
  const { user, setUser, cookie } = props;
  if (typeof user === 'undefined' && cookie) {
    setUser(cookie);
  }
  let Component = OfficerHome;
  if (user && user.role) {
    if (user.role === 2) {
      Component = StudentHome;
    } else if (user.role === 1) {
      Component = LandlordHome;
    }
  }
  return <div>{user && user.role >= 0 ? <Component /> : null}</div>;
}

function mapStateToProps(state) {
  const { user, cookie } = state.users;
  return {
    user,
    cookie
  };
}

const mapDispatchToProps = dispatch => {
  return {
    setUser: token => {
      dispatch(userActions.setUser(token));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Home);

import React from 'react';
import { connect } from 'react-redux';
import StudentHome from './StudentHome';
import LandlordHome from './LandlordHome';
import OfficerHome from './OfficerHome';

function Home(props) {
  const { user } = props;
  return (
    <div>
      {user && user.role === 2 ? (
        <StudentHome />
      ) : user.role === 1 ? (
        <LandlordHome />
      ) : (
        <OfficerHome />
      )}
    </div>
  );
}

function mapStateToProps(state) {
  const { user } = state.users;
  return {
    user
  };
}

export default connect(mapStateToProps)(Home);

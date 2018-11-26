import React from 'react';
import { connect } from 'react-redux';
import StudentHome from './StudentHome';
import LandlordHome from './LandlordHome';
import OfficerHome from './OfficerHome';

function Home(props) {
  const { user } = props;
  return (
    <div>
      {user && user.role === 'student' ? (
        <StudentHome />
      ) : user.role === 'landlord' ? (
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

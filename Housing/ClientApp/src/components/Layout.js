import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './NavMenu';
import './Layout.css';
import { connect } from 'react-redux';
import { userActions } from '../store/actions';

function Layout(props) {
  return (
    <div className="layout_container">
      {props.loggedIn && !props.loggingIn ? (
        <NavMenu
          logout={() => {
            props.logout();
          }}
        />
      ) : null}
      <Grid fluid className="layout_body">
        <Row>{props.children}</Row>
      </Grid>
    </div>
  );
}

function mapStateToProps(state) {
  return {
    loggedIn: !!state.users.user,
    loggingIn: state.users.loggingIn
  };
}

const mapDispatchToProps = dispatch => {
  return {
    logout: () => {
      dispatch(userActions.logout());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Layout);

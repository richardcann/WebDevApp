import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './NavMenu';
import './Layout.css';
import { connect } from 'react-redux';

function Layout(props) {
  return (
    <div className="layout_container">
      {props.loggedIn && !props.loggingIn ? <NavMenu /> : null}
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

export default connect(mapStateToProps)(Layout);

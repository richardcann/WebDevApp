import React from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
//import './NavMenu.css';

export default props => (
  <Navbar inverse fixedTop fluid staticTop collapseOnSelect>
    <Navbar.Header>
      <Navbar.Brand>
        <Link to={'/'}>Housing</Link>
      </Navbar.Brand>
      <Navbar.Toggle />
    </Navbar.Header>
    <Navbar.Collapse>
      <Nav>
        <LinkContainer to={'/'} exact>
          <NavItem>
            <Glyphicon glyph="home" /> Home
          </NavItem>
        </LinkContainer>
        <NavItem onClick={props.logout}>
          <Glyphicon glyph="th-list" /> Logout
        </NavItem>
      </Nav>
    </Navbar.Collapse>
  </Navbar>
);

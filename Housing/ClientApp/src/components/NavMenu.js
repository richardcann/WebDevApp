import React from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
//import './NavMenu.css';
import 'antd/lib/menu/style/css';
import 'antd/lib/icon/style/css';
import { Menu, Icon } from 'antd';

const SubMenu = Menu.SubMenu;
const MenuItemGroup = Menu.ItemGroup;

export default props => (
  <Navbar inverse fixedTop fluid staticTop collapseOnSelect>
    <Navbar.Header>
      <Navbar.Brand>
        <Link to={'/homr'}>Housing</Link>
      </Navbar.Brand>
      <Navbar.Toggle />
    </Navbar.Header>
    <Navbar.Collapse>
      <Nav>
        <LinkContainer to={'/home'} exact>
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

/*export default props => (
    <Menu
        onClick={this.handleClick}
        selectedKeys={['mail']}
        mode="horizontal"
    >
      <Menu.Item key="mail">
        <Icon type="mail" /><Link to={'/'}>Housing</Link>
      </Menu.Item>
      <Menu.Item key="app" >
        <Icon type="appstore" /><Link to={'/'}>Home</Link>
      </Menu.Item>
      <Menu.Item key="alipay">
        <a href="https://ant.design" target="_blank" rel="noopener noreferrer">Navigation Four - Link</a>
      </Menu.Item>
    </Menu>
);*/

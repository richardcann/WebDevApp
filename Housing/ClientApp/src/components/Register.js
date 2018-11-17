import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { userActions } from '../store/actions';

class Register extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      user: {
        firstName: '',
        lastName: '',
        username: '',
        password: ''
      },
      submitted: false
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    const { name, value } = event.target;
    const { user } = this.state;
    this.setState({
      user: {
        ...user,
        [name]: value
      }
    });
  }

  handleSubmit(event) {
    event.preventDefault();

    this.setState({ submitted: true });
    const { user } = this.state;
    const { register } = this.props;
    if (
      user.firstName &&
      user.lastName &&
      user.username &&
      user.password &&
      user.role &&
      user.email
    ) {
      register(user);
    }
  }

  render() {
    const { registering } = this.props;
    const { user, submitted } = this.state;
    return (
      <div>
        <h2>Register</h2>
        <form name="form" onSubmit={this.handleSubmit}>
          <div
            className={
              'form-group' + (submitted && !user.firstName ? ' has-error' : '')
            }
          >
            <label htmlFor="firstName">First Name</label>
            <input
              type="text"
              className="form-control"
              name="firstName"
              value={user.firstName}
              onChange={this.handleChange}
            />
            {submitted &&
              !user.firstName && (
                <div className="help-block">First Name is required</div>
              )}
          </div>
          <div
            className={
              'form-group' + (submitted && !user.lastName ? ' has-error' : '')
            }
          >
            <label htmlFor="lastName">Last Name</label>
            <input
              type="text"
              className="form-control"
              name="lastName"
              value={user.lastName}
              onChange={this.handleChange}
            />
            {submitted &&
              !user.lastName && (
                <div className="help-block">Last Name is required</div>
              )}
          </div>
          <div
            className={
              'form-group' + (submitted && !user.role ? ' has-error' : '')
            }
          >
            <label htmlFor="role">Role</label>
            <select
              className="form-control"
              name="role"
              value={user.role}
              onChange={this.handleChange}
            >
              <option style={{ display: 'none' }}>Pick Role</option>
              <option value="student">Student</option>
              <option value="officer">Uni Officer</option>
              <option value="landlord">Landlord</option>
            </select>
            {submitted &&
              !user.role && <div className="help-block">Role is required</div>}
          </div>
          <div
            className={
              'form-group' + (submitted && !user.username ? ' has-error' : '')
            }
          >
            <label htmlFor="username">Username</label>
            <input
              type="text"
              className="form-control"
              name="username"
              value={user.username}
              onChange={this.handleChange}
            />
            {submitted &&
              !user.username && (
                <div className="help-block">Username is required</div>
              )}
          </div>
          <div
            className={
              'form-group' + (submitted && !user.email ? ' has-error' : '')
            }
          >
            <label htmlFor="email">Email</label>
            <input
              type="text"
              className="form-control"
              name="email"
              value={user.email}
              onChange={this.handleChange}
            />
            {submitted &&
              !user.email && (
                <div className="help-block">Email is required</div>
              )}
          </div>
          <div
            className={
              'form-group' + (submitted && !user.password ? ' has-error' : '')
            }
          >
            <label htmlFor="password">Password</label>
            <input
              type="password"
              className="form-control"
              name="password"
              value={user.password}
              onChange={this.handleChange}
            />
            {submitted &&
              !user.password && (
                <div className="help-block">Password is required</div>
              )}
          </div>
          <div className="form-group">
            <button className="btn btn-primary">Register</button>
            {registering && (
              <img src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
            )}
            <Link to="/login" className="btn btn-link">
              Cancel
            </Link>
          </div>
        </form>
      </div>
    );
  }
}

function mapStateToProps(state) {
  const { registering } = state.users;
  return {
    registering
  };
}

const mapDispatchToProps = dispatch => {
  return {
    register: user => {
      dispatch(userActions.register(user));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Register);

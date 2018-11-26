import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actions } from '../store/actions';
import PropertyCard from './PropertyCard';

const Counter = props => (
  <div>
    <h1>Counter</h1>

    <p>This is a simple example of a React component.</p>

    <p>
      Current count: <strong>{props.count}</strong>
    </p>

    <button onClick={props.increment}>Increment</button>
    <button onClick={props.decrement}>Decrement</button>
  </div>
);

const mapStateToProps = state => {
  return {
    ...state.counter
  };
};

const mapDispatchToProps = dispatch => {
  return {
    increment: () => {
      dispatch(actions.increment());
    },
    decrement: () => {
      dispatch(actions.decrement());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Counter);

/*export default connect(
  state => state.counter,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Counter);*/

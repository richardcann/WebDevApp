import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';
import { meal } from './meal';
import { counter } from './counter';
import { weatherForecasts } from './weatherForecasts';
import { users } from './user';

const mealAppReducers = combineReducers({
  meal,
  counter,
  weatherForecasts,
  users,
  routing: routerReducer
});

export default mealAppReducers;

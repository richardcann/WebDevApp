import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';
import { meal } from './meal';
import { counter } from './counter';
import { weatherForecasts } from './weatherForecasts';
import { users } from './user';
import { landlord } from './landlord';
import { officer } from './officer';

const mealAppReducers = combineReducers({
  meal,
  counter,
  weatherForecasts,
  users,
  landlord,
  officer,
  routing: routerReducer
});

export default mealAppReducers;

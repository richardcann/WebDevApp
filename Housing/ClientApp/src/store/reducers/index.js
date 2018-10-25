import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';
import { meal } from './meal';
import { counter } from './counter';
import { weatherForecasts } from './weatherForecasts';

const mealAppReducers = combineReducers({
  meal,
  counter,
  weatherForecasts,
  routing: routerReducer
});

export default mealAppReducers;

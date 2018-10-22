const requestWeatherForecastsType = 'REQUEST_WEATHER_FORECASTS';
const receiveWeatherForecastsType = 'RECEIVE_WEATHER_FORECASTS';

export const changeMain = text => {
  return {
    type: 'CHANGE_MAIN',
    text
  }
};

export const changeSide = text => {
  return {
    type: 'CHANGE_SIDE',
    text
  }
};

export const changeDrink = text => {
  return {
    type: 'CHANGE_DRINK',
    text
  }
};

export const increment = () => {
  return {
    type: 'INCREMENT_COUNT'
  }
};

export const decrement = () => {
  return {
    type: 'DECREMENT_COUNT'
  }
};

export const actionCreators = {
  requestWeatherForecasts: startDateIndex => async (dispatch, getState) => {
    if (startDateIndex === getState().weatherForecasts.startDateIndex) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: requestWeatherForecastsType, startDateIndex });

    const url = `api/SampleData/WeatherForecasts?startDateIndex=${startDateIndex}`;
    const response = await fetch(url);
    const forecasts = await response.json();

    dispatch({ type: receiveWeatherForecastsType, startDateIndex, forecasts });
  }
};

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import './index.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import configureStore from './store/configureStore';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import Cookies from 'universal-cookie';

// Create browser history to use in the Redux store
/*const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });*/
import { history } from './store/helpers/history';
import { configureFakeBackend } from './store/helpers/fakeBackend';

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const cookies = new Cookies();
const cookie = cookies.get('Token');
const store = configureStore(history, { ...initialState, users: { cookie } });
localStorage.clear();

const rootElement = document.getElementById('root');
configureFakeBackend();

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  rootElement
);

registerServiceWorker();

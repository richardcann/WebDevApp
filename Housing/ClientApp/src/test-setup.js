// needed for regenerator-runtime
// (ES7 generator support is required by redux-saga)

// Enzyme adapter for React 16
import Enzyme from 'enzyme';
import Adapter from 'enzyme-adapter-react-15';

Enzyme.configure({ adapter: new Adapter() });

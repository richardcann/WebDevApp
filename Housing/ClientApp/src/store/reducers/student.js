const initialState = {};

export function student(state, action) {
  state = state || initialState;
  switch (action.type) {
    case 'SET_PROPERTIES':
      return {
        ...state,
        properties: action.properties
      };
    default:
      return state;
  }
}

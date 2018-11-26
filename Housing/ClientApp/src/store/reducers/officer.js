const initialState = {};

export function officer(state, action) {
  state = state || initialState;
  switch (action.type) {
    case 'SET_PROPERTIES':
      return {
        ...state,
        currentProperties: action.properties
      };
    case 'DISAPPROVE_PROPERTY':
      return {
        ...state,
        disapprovedProperty: action.index
      };
    case 'SUBMIT_DISAPPROVAL':
      return {
        ...state,
        disapprovedProperty: null
      };
    default:
      return state;
  }
}

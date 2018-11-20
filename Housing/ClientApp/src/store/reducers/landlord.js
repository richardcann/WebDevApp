const initialState = {};

export function landlord(state, action) {
  state = state || initialState;
  switch (action.type) {
    case 'ADD_NEW_PROPERTY':
      return {
        ...state,
        addingProperty: true
      };
    case 'PROPERTY_ADDED':
      return {
        ...state,
        addingProperty: false
      };
    case 'EDIT_PROPERTY':
      return {
        ...state,
        editingProperty: action.property
      };
    case 'SUBMIT_PROPERTY':
      return {
        ...state,
        editingProperty: null
      };
    case 'SET_PROPERTIES':
      return {
        ...state,
        currentProperties: action.properties
      };
    case 'SHOW_DISAPPROVED':
      return {
        ...state,
        showDisapproved: action.index
      };
    case 'HIDE_DISAPPROVED':
      return {
        ...state,
        showDisapproved: null
      };
    default:
      return state;
  }
}

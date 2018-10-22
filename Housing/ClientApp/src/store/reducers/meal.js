const initialState = {main: null, side: null, drink: null};

export const meal = (state, action) => {
  state = state || initialState;


  if (action.type === 'CHANGE_MAIN') {
    return { ...state, main: action.text };
  }

  if (action.type === 'CHANGE_SIDE') {
    return { ...state, side: action.text };
  }

  if (action.type === 'CHANGE_DRINK') {
    return { ...state, drink: action.text };
  }

  return state;
};

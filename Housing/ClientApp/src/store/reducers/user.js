let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? { loggedIn: true, user } : {};

export function users(state, action) {
  state = state || initialState;
  switch (action.type) {
    case 'GETALL_REQUEST':
      return {
        loading: true
      };
    case 'GETALL_SUCCESS':
      return {
        items: action.users
      };
    case 'GETALL_FAILURE':
      return {
        error: action.error
      };
    case 'DELETE_REQUEST':
      // add 'deleting:true' property to user being deleted
      return {
        ...state,
        items: state.items.map(
          user => (user.id === action.id ? { ...user, deleting: true } : user)
        )
      };
    case 'DELETE_SUCCESS':
      // remove deleted user from state
      return {
        items: state.items.filter(user => user.id !== action.id)
      };
    case 'DELETE_FAILURE':
      // remove 'deleting:true' property and add 'deleteError:[error]' property to user
      return {
        ...state,
        items: state.items.map(user => {
          if (user.id === action.id) {
            // make copy of user without 'deleting:true' property
            const { deleting, ...userCopy } = user;
            // return copy of user with 'deleteError:[error]' property
            return { ...userCopy, deleteError: action.error };
          }

          return user;
        })
      };
    case 'REGISTER_REQUEST':
      return { registering: true };
    case 'REGISTER_SUCCESS':
      return {};
    case 'REGISTER_FAILURE':
      return {};
    case 'LOGIN_REQUEST':
      return {
        loggingIn: true,
        user: action.user
      };
    case 'LOGIN_SUCCESS':
      return {
        loggedIn: true,
        user: action.user
      };
    case 'LOGIN_FAILURE':
      return {};
    case 'LOGOUT':
      return {};
    default:
      return state;
  }
}

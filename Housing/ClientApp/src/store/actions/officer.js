export function setProperties(properties) {
  return { type: 'SET_PROPERTIES', properties };
}

export function disapproveProperty(index) {
  return { type: 'DISAPPROVE_PROPERTY', index };
}

export function submitDisapproval() {
  return { type: 'SUBMIT_DISAPPROVAL' };
}

export function addNewProperty() {
  return { type: 'ADD_NEW_PROPERTY' };
}

export function propertyAdded() {
  return { type: 'PROPERTY_ADDED' };
}

export function editProperty(property) {
  return { type: 'EDIT_PROPERTY', property };
}

export function submitProperty() {
  return { type: 'SUBMIT_PROPERTY' };
}

export function setProperties(properties) {
  return { type: 'SET_PROPERTIES', properties };
}

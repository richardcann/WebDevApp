import * as actions from './index';
import { exampleProperty } from '../../components/sampleConstants';

describe('Actions', () => {
  describe('landlord', () => {
    it('should set categories', () => {
      const properties = [exampleProperty];
      const expectedResult = {
        type: 'SET_PROPERTIES',
        properties
      };

      expect(actions.landlordActions.setProperties(properties)).toEqual(
        expectedResult
      );
    });

    it('should add new property', () => {
      const expectedResult = {
        type: 'ADD_NEW_PROPERTY'
      };

      expect(actions.landlordActions.addNewProperty()).toEqual(expectedResult);
    });

    it('should act as property added', () => {
      const expectedResult = {
        type: 'PROPERTY_ADDED'
      };

      expect(actions.landlordActions.propertyAdded()).toEqual(expectedResult);
    });

    it('should edit property', () => {
      const property = [{ ...exampleProperty, id: 0 }];
      const expectedResult = {
        type: 'EDIT_PROPERTY',
        property
      };

      expect(actions.landlordActions.editProperty(property)).toEqual(
        expectedResult
      );
    });

    it('should show disapproved', () => {
      const index = 0;
      const expectedResult = {
        type: 'SHOW_DISAPPROVED',
        index
      };

      expect(actions.landlordActions.showCurrentDisapproved(index)).toEqual(
        expectedResult
      );
    });

    it('should hide disapporved property', () => {
      const expectedResult = {
        type: 'HIDE_DISAPPROVED'
      };

      expect(actions.landlordActions.hideDisapproved()).toEqual(expectedResult);
    });

    it('should cancel modal', () => {
      const expectedResult = {
        type: 'CANCEL_MODAL'
      };

      expect(actions.landlordActions.cancelModal()).toEqual(expectedResult);
    });
  });

  describe('officer', () => {
    it('should set categories', () => {
      const properties = [exampleProperty];
      const expectedResult = {
        type: 'SET_PROPERTIES',
        properties
      };

      expect(actions.officerActions.setProperties(properties)).toEqual(
        expectedResult
      );
    });

    it('should disapprove index', () => {
      const index = 0;
      const expectedResult = {
        type: 'DISAPPROVE_PROPERTY',
        index
      };

      expect(actions.officerActions.disapproveProperty(index)).toEqual(
        expectedResult
      );
    });

    it('should cancel modal', () => {
      const expectedResult = {
        type: 'SUBMIT_DISAPPROVAL'
      };

      expect(actions.officerActions.closeModal()).toEqual(expectedResult);
    });
  });
});

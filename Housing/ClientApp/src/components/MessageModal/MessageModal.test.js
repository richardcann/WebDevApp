import React from 'react';
import MessageModel from './';
import ReactDOM from 'react-dom';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MessageModel
      visible={true}
      onCancel={() => {}}
      onSubmit={() => {}}
      title={'testTitle'}
    />,
    div
  );
});

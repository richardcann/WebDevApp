import 'antd/lib/form/style/css';
import 'antd/lib/input/style/css';
import 'antd/lib/modal/style/css';
import { Form, Input, Modal } from 'antd';
import React from 'react';

const { TextArea } = Input;

class Demo extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      property: this.props.property
    };
  }

  render() {
    const { visible, onCancel, title, message } = this.props;
    return (
      <Modal
        visible={visible}
        title={`${title} left this message:`}
        okText="Ok"
        onCancel={onCancel}
      >
        {/*<TextArea value={message ? message : ''} disabled={!!message} onChange={() => {}}/>*/}
        <p>{message}</p>
      </Modal>
    );
  }
}

export default Form.create()(Demo);

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
      message: this.props.message,
      notEdit: this.props.noEdit
    };
  }

  render() {
    const { visible, onCancel, title, onSubmit } = this.props;
    const { message, notEdit } = this.state;
    return (
      <Modal
        visible={visible}
        title={`${title}`}
        okText="Ok"
        onCancel={onCancel}
        onOk={() => (onSubmit ? onSubmit(message) : onCancel())}
      >
        {message && notEdit ? (
          <p>{message}</p>
        ) : (
          <TextArea
            value={message ? message : ''}
            onChange={e => {
              this.setState({ message: e.target.value });
            }}
          />
        )}
      </Modal>
    );
  }
}

export default Form.create()(Demo);

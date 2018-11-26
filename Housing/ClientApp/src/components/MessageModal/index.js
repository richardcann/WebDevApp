import 'antd/lib/form/style/css';
import 'antd/lib/input/style/css';
import 'antd/lib/modal/style/css';
import { Form, Input, Modal } from 'antd';
import React from 'react';

const FormItem = Form.Item;

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
    const { getFieldDecorator } = this.props.form;
    const formItemLayout = {
      labelCol: { span: 10 },
      wrapperCol: { span: 14 }
    };
    const { message, notEdit } = this.state;
    const currentMessage = message ? message : '';
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
          <Form>
            <FormItem {...formItemLayout} label="Reason">
              {getFieldDecorator('reason', {
                rules: [
                  {
                    required: true,
                    message:
                      'Please input message between 50 and 200 characters',
                    max: 200,
                    min: 50
                  }
                ],
                initialValue: currentMessage
              })(
                <TextArea
                  onChange={e => {
                    this.setState({ message: e.target.value });
                  }}
                />
              )}
            </FormItem>
          </Form>
        )}
      </Modal>
    );
  }
}

export default Form.create()(Demo);

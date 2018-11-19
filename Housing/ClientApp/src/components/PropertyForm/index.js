import 'antd/lib/form/style/css';
import 'antd/lib/input/style/css';
import 'antd/lib/button/style/css';
import 'antd/lib/upload/style/css';
import 'antd/lib/icon/style/css';
import 'antd/lib/modal/style/css';
import { Form, Input, Button, Upload, Icon, Modal } from 'antd';
import React from 'react';

const FormItem = Form.Item;
const { TextArea } = Input;

class Demo extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      property: this.props.property
    };
  }

  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        console.log('Received values of form: ', values);
      }
      console.log(values);
      this.props.onSubmit(values);
    });
  };

  normFile = e => {
    console.log('Upload event:', e);
    if (Array.isArray(e)) {
      return e;
    }
    return e && e.fileList;
  };

  /*onFormChange = (value, index) => {
    let prop = {...this.state.property};
    prop[index] = value;
    console.log(prop);
    this.setState({property: {...prop}})
  };*/

  render() {
    const { getFieldDecorator } = this.props.form;
    const { visible, onCancel, property } = this.props;
    const formItemLayout = {
      labelCol: { span: 10 },
      wrapperCol: { span: 14 }
    };
    const currentTitle = property ? property.title : '';
    const currentDescription = property ? property.description : '';
    return (
      <Modal
        visible={visible}
        title="Enter Property Details"
        okText="Create"
        onCancel={onCancel}
        onOk={this.handleSubmit}
      >
        <Form>
          <FormItem {...formItemLayout} label="Title">
            {getFieldDecorator('title', {
              rules: [
                { required: true, message: 'Please input property title' }
              ],
              initialValue: currentTitle
            })(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="Address">
            {getFieldDecorator('address', {
              rules: [
                { required: true, message: 'Please input property address' }
              ],
              initialValue: currentTitle
            })(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="City">
            <Input />
          </FormItem>
          <FormItem {...formItemLayout} label="Postcode">
            <Input />
          </FormItem>

          <FormItem {...formItemLayout} label="Description">
            {getFieldDecorator('description', {
              rules: [
                { required: true, message: 'Please input property description' }
              ],
              initialValue: currentDescription
            })(<TextArea />)}
          </FormItem>

          <FormItem {...formItemLayout} label="Upload photo" extra="">
            {getFieldDecorator('upload', {
              valuePropName: 'fileList',
              getValueFromEvent: this.normFile
            })(
              <Upload name="logo" action="/upload.do" listType="picture">
                <Button>
                  <Icon type="upload" /> Click to upload
                </Button>
              </Upload>
            )}
          </FormItem>
        </Form>
      </Modal>
    );
  }
}

export default Form.create()(Demo);

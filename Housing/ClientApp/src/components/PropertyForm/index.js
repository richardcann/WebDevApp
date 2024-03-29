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
      const imagesUrl = values.images
        ? values.images.map(image => {
            return image.thumbUrl;
          })
        : [];
      this.props.onSubmit({ ...values, images: imagesUrl });
    });
  };

  normFile = e => {
    console.log('Upload event:', e);
    if (Array.isArray(e)) {
      return e;
    }
    return e && e.fileList;
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { visible, onCancel, property } = this.props;
    const formItemLayout = {
      labelCol: { span: 10 },
      wrapperCol: { span: 14 }
    };
    const currentTitle = property ? property.addressLine1 : '';
    const addressLine2 = property ? property.addressLine2 : '';
    const city = property ? property.city : '';
    const postcode = property ? property.postcode : '';
    const county = property ? property.county : '';
    const currentDescription = property ? property.propertyDescription : '';
    return (
      <Modal
        visible={visible}
        title="Enter Property Details"
        okText="Create"
        onCancel={onCancel}
        onOk={this.handleSubmit}
      >
        <Form>
          <FormItem {...formItemLayout} label="Address">
            {getFieldDecorator('addressLine1', {
              rules: [
                { required: true, message: 'Please input property address' }
              ],
              initialValue: currentTitle
            })(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="Address">
            {getFieldDecorator('addressLine2', {
              initialValue: addressLine2
            })(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="City">
            {getFieldDecorator('city', {
              rules: [
                {
                  required: true,
                  message: 'Please input city'
                }
              ],
              initialValue: city
            })(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="County">
            {getFieldDecorator('county', {
              rules: [
                {
                  required: true,
                  message: 'Please input county'
                }
              ],
              initialValue: county
            })(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="Postcode">
            {getFieldDecorator('postcode', {
              rules: [
                {
                  required: true,
                  message: 'Please input postcode',
                  pattern: /^[A-Za-za-z]{2}\d{1,2}\s*\d[A-Za-za-z]{2}$/
                }
              ],
              initialValue: postcode
            })(<Input />)}
          </FormItem>

          <FormItem {...formItemLayout} label="Description">
            {getFieldDecorator('propertyDescription', {
              rules: [
                {
                  required: true,
                  message:
                    'Please input property description between 50 and 200 characters',
                  max: 200,
                  min: 50
                }
              ],
              initialValue: currentDescription
            })(<TextArea />)}
          </FormItem>

          <FormItem {...formItemLayout} label="Upload photo" extra="">
            {getFieldDecorator('images', {
              valuePropName: 'fileList',
              getValueFromEvent: this.normFile
            })(
              <Upload
                name="logo"
                action={f => {
                  return Promise.resolve();
                }}
                listType="picture"
              >
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

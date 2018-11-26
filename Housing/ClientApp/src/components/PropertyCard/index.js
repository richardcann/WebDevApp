import 'antd/lib/card/style/css';
import 'antd/lib/carousel/style/css';
import { Card } from 'antd';
import React from 'react';
import MasonryLayout from '../MasonryLayout';
import MapCard from '../MapCard';
import { Carousel } from 'antd';

const { Meta } = Card;

const tabList = [
  {
    key: 'Overview',
    tab: 'Overview'
  },
  {
    key: 'Photos',
    tab: 'Photos'
  },
  {
    key: 'Map',
    tab: 'Map'
  }
];

function OverviewCard(props) {
  const { title, description, photoUrl } = props;
  return (
    <Card
      hoverable
      style={{ width: '100%' }}
      cover={<img alt="example" src={photoUrl} />}
    >
      <Meta title={title} description={description} />
    </Card>
  );
}

function PhotosCard(props) {
  const { photos } = props;
  return (
    <Carousel effect="fade" autoplay>
      {photos.map(photo => {
        return <img alt="example" src={photo} />;
      })}
    </Carousel>
  );
}

class PropertyCard extends React.Component {
  state = {
    key: 'Overview'
  };

  onTabChange = (key, type) => {
    console.log(key, type);
    this.setState({ [type]: key });
  };

  render() {
    const { property, id } = this.props;
    const {
      addressLine1,
      propertyDescription,
      images,
      latitude,
      longitude
    } = property;
    const contentList = {
      Overview: (
        <OverviewCard
          title={addressLine1}
          description={propertyDescription}
          photoUrl={images ? images[0] : ''}
        />
      ),
      Photos: <PhotosCard photos={images} />,
      Map: <MapCard id={id} position={[latitude, longitude]} />
    };
    return (
      <div style={{ width: '40em' }}>
        <Card
          style={{ width: '100%' }}
          title={addressLine1}
          extra={this.props.extra}
          tabList={tabList}
          activeTabKey={this.state.key}
          onTabChange={key => {
            this.onTabChange(key, 'key');
          }}
        >
          {contentList[this.state.key]}
        </Card>
        <br />
        <br />
      </div>
    );
  }
}

export default PropertyCard;

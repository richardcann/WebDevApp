import 'antd/lib/card/style/css';
import { Card } from 'antd';
import React from 'react';
import MasonryLayout from '../MasonryLayout';
import MapCard from '../MapCard';

const { Meta } = Card;

const tabList = [
  {
    key: 'overview',
    tab: 'overview'
  },
  /*{
    key: 'photos',
    tab: 'photos'
  },*/
  {
    key: 'map',
    tab: 'map'
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
    <MasonryLayout columns={2} gap={25}>
      {photos.map(photo => {
        const height = 200 + Math.ceil(Math.random() * 300);

        return (
          <img
            style={{ height: `${height}px`, maxWidth: '100%' }}
            src={photo.url}
          />
        );
      })}
    </MasonryLayout>
  );
}

class PropertyCard extends React.Component {
  state = {
    key: 'overview'
  };

  onTabChange = (key, type) => {
    console.log(key, type);
    this.setState({ [type]: key });
  };

  render() {
    const { property } = this.props;
    const { title, description, photos, position } = property;
    const contentList = {
      overview: (
        <OverviewCard
          title={title}
          description={description}
          photoUrl={photos ? photos[0].url : ''}
        />
      ),
      //photos: <PhotosCard photos={photos} />,
      map: <MapCard id={this.props.id} />
    };
    return (
      <div style={{ width: '40em' }}>
        <Card
          style={{ width: '100%' }}
          title={title}
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

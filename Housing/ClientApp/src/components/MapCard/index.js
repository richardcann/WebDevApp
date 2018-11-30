import React from 'react';
import L from 'leaflet';

const style = {
  height: '300px'
};

class PropMap extends React.Component {
  state = {
    lat: this.props.position[0],
    lng: this.props.position[1]
  };
  componentDidMount() {
    // create map
    this.map = L.map(this.props.id, {
      center: this.props.position,
      zoom: 16,
      layers: [
        L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
          attribution:
            '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        })
      ]
    });

    // add marker
    this.marker = L.marker(this.state).addTo(this.map);
  }
  componentDidUpdate({ markerPosition }) {
    // check if position has changed
    const position = this.state;
    if (position !== markerPosition) {
      this.marker.setLatLng(position);
    }
  }
  render() {
    return <div id={this.props.id} style={style} />;
  }
}

export default PropMap;

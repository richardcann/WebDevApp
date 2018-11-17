/*import React, { Component } from 'react'
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';

export default class MapCard extends Component {
  state = {
    lat: 51.505,
    lng: -0.09,
    zoom: 13,
  };

  render() {
    const position = [this.state.lat, this.state.lng];
    return (
        <div style={{height: '8em', width:'100%'}}>
          <Map center={position} zoom={this.state.zoom}>
            <TileLayer
                attribution="&amp;copy <a href=&quot;http://osm.org/copyright&quot;>OpenStreetMap</a> contributors"
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <Marker position={position}>
              <Popup>
                A pretty CSS3 popup. <br /> Easily customizable.
              </Popup>
            </Marker>
          </Map>
        </div>
    )
  }
}*/
import React from 'react';
import L from 'leaflet';

const style = {
  height: '300px'
};

class PropMap extends React.Component {
  state = {
    lat: 51.505,
    lng: -0.09
  };
  componentDidMount() {
    // create map
    this.map = L.map(this.props.id, {
      center: [51.505, -0.09],
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

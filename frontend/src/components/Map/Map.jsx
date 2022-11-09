import React from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import './Map.css';
import * as L from 'leaflet';

import LocationMarker from './LocationMarker';
import SearchControl from './SearchControl';

function Map() {
  const [position, setPosition] = React.useState(null);

  const getPosition = (_position) => {
    console.log(_position);
    setPosition(_position);
  };

  const redIcon = new L.Icon({
    iconUrl:
      'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
  });

  return (
    <>
      <SearchControl getPosition={getPosition} />
      <MapContainer className={'map'} center={[50.9274158, 6.9961041]} zoom={13}>
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <Marker icon={redIcon} position={[50.9274158, 6.9961041]}>
          <Popup>Georg-Simon-Ohm Berufskolleg</Popup>
        </Marker>
        {!!position && <LocationMarker position={position} />}
      </MapContainer>
    </>
  );
}

export default Map;

import React from 'react';
import { Marker, Popup, useMap } from 'react-leaflet';
import * as L from 'leaflet';

function LocationMarker(position) {
  const map = useMap();

  const greenIcon = new L.Icon({
    iconUrl:
      'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
  });

  if (position.position.length > 0) {
    const latLng = {};
    latLng.lng = position.position[0];
    latLng.lat = position.position[1];

    map.flyTo(latLng);
  }

  return position.position.length > 0 ? (
    <Marker icon={greenIcon} position={[position.position[1], position.position[0]]}>
      <Popup>Du bist hier</Popup>
    </Marker>
  ) : null;
}

export default LocationMarker;

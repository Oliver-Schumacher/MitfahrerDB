import React from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import './Map.css';
import {
  Radio,
  FormControlLabel,
  RadioGroup,
  MenuItem,
  TextField,
  Button,
  InputLabel,
  Box,
  FormControl,
  OutlinedInput
} from '@mui/material';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import axios from 'axios';

function Map() {
  const [values, setValues] = React.useState({
    street: '',
    postal: '',
    city: '',
    hour: '',
    rideType: '',
    date: null
  });

  const hours = [
    {
      value: '1',
      label: '1'
    },
    {
      value: '2',
      label: '2'
    },
    {
      value: '3',
      label: '3'
    },
    {
      value: '4',
      label: '4'
    },
    {
      value: '5',
      label: '5'
    },
    {
      value: '6',
      label: '6'
    },
    {
      value: '7',
      label: '7'
    },
    {
      value: '8',
      label: '8'
    },
    {
      value: '9',
      label: '9'
    },
    {
      value: '10',
      label: '10'
    },
    {
      value: '11',
      label: '11'
    },
    {
      value: '12',
      label: '12'
    }
  ];

  const handleChange = (prop) => (event) => {
    setValues({ ...values, [prop]: event.target.value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log(values);
    const address = `${values.street} ${values.postal} ${values.city}`;
    const params = {
      access_key: '455306e3a325e5f227e4fdea2815707a',
      query: address
    };
    axios
      .get(`http://api.positionstack.com/v1/forward`, { params })
      .then((response) => {
        console.log(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  };
  return (
    <>
      <Box
        component="form"
        onSubmit={handleSubmit}
        noValidate
        sx={{
          alignItems: 'center',
          justifyContent: 'space-evenly',
          flexWrap: 'wrap',
          display: 'flex',
          mt: 2,
          mb: 2
        }}>
        <FormControl>
          <RadioGroup
            value={values.rideType}
            onChange={handleChange('rideType')}
            aria-labelledby="ride-type"
            name="ride-type">
            <FormControlLabel value="Hinfahrt" control={<Radio />} label="Hinfahrt" />
            <FormControlLabel value="Rückfahrt" control={<Radio />} label="Rückfahrt" />
          </RadioGroup>
        </FormControl>
        <LocalizationProvider dateAdapter={AdapterMoment}>
          <DatePicker
            label="Datum"
            value={values.date}
            onChange={(newValue) => {
              setValues({ ...values, date: newValue.format() });
            }}
            renderInput={(params) => <TextField {...params} />}
          />
        </LocalizationProvider>
        <TextField
          sx={{ minWidth: 90 }}
          id="hour"
          select
          label="Stunde"
          name="hour"
          value={values.hour}
          onChange={handleChange('hour')}>
          {hours.map((option) => (
            <MenuItem key={option.value} value={option.value}>
              {option.label}
            </MenuItem>
          ))}
        </TextField>
        <FormControl>
          <InputLabel htmlFor="outlined-adornment-street">Straße</InputLabel>
          <OutlinedInput
            id="outlined-adornment-street"
            value={values.street}
            onChange={handleChange('street')}
            label="street"
          />
        </FormControl>
        <FormControl sx={{ maxWidth: 80 }}>
          <InputLabel htmlFor="outlined-adornment-postal">PLZ</InputLabel>
          <OutlinedInput
            id="outlined-adornment-postal"
            value={values.postal}
            onChange={handleChange('postal')}
            label="postal"
          />
        </FormControl>
        <FormControl>
          <InputLabel htmlFor="outlined-adornment-city">Ort</InputLabel>
          <OutlinedInput
            id="outlined-adornment-city"
            value={values.city}
            onChange={handleChange('city')}
            label="city"
          />
        </FormControl>
        <Button type="submit" variant="contained">
          Suchen
        </Button>
      </Box>
      <MapContainer className={'map'} center={[50.9274158, 6.9961041]} zoom={13}>
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <Marker position={[50.9274158, 6.9961041]}>
          <Popup>Georg-Simon-Ohm Berufskolleg</Popup>
        </Marker>
      </MapContainer>
    </>
  );
}

export default Map;

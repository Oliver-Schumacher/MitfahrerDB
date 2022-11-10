import React from 'react';
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
  OutlinedInput,
  FormGroup,
  Switch
} from '@mui/material';
import axios from 'axios';

const SearchControl = (props) => {
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
  const weekdays = ['Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag'];
  const [values, setValues] = React.useState({
    street: '',
    postal: '',
    city: '',
    hour: '',
    rideType: '',
    weekDay: '',
    sameGender: false
  });
  const [position, setPosition] = React.useState([]);
  const [trips, setTrips] = React.useState([]);
  const [filteredTrips, setFilteredTrips] = React.useState([]);

  React.useEffect(() => {
    axios.get(`https://localhost:7200/Trips`).then((res) => {
      const response = res.data;
      setTrips(response);
    });
  }, []);

  React.useEffect(() => {
    props.getPosition(position);
  }, [position]);

  React.useEffect(() => {
    props.getTrips(filteredTrips);
  }, [filteredTrips]);

  const handleChange = (prop) => (event) => {
    setValues({ ...values, [prop]: event.target.value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();

    const address = `${values.street} ${values.postal} ${values.city}`;

    const newArr = trips.filter(
      (trip) => trip.sameGender === values.sameGender && trip.weekDay === values.weekDay
    );

    setFilteredTrips(newArr);

    const access_token =
      'pk.eyJ1IjoiZmFkZTEzMDkiLCJhIjoiY2xhOWVkbTh5MGRpMTNxcW9oeWN5NGxoYyJ9.POjmItIyWziDT51NqsYohg';

    axios
      .get(
        `https://api.mapbox.com/geocoding/v5/mapbox.places/${address}.json?country=de&limit=3&proximity=ip&types=place&language=de&access_token=${access_token}`
      )
      .then((response) => {
        setPosition(response.data.features[0].center);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
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
      <FormGroup>
        <FormControlLabel
          labelPlacement="top"
          control={
            <Switch
              onChange={(event) => setValues({ ...values, sameGender: event.target.checked })}
              value={values.sameGender}
            />
          }
          label="Nur gleiches Geschlecht"
        />
      </FormGroup>

      <TextField
        sx={{ minWidth: 140 }}
        id="weekday"
        select
        label="Wochentag"
        name="weekday"
        value={values.weekDay}
        onChange={handleChange('weekDay')}>
        {weekdays.map((weekday) => (
          <MenuItem key={weekday} value={weekday}>
            {weekday}
          </MenuItem>
        ))}
      </TextField>
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
      <Button type="submit" fullWidth variant="contained" sx={{ mr: 30, ml: 30 }}>
        Suchen
      </Button>
    </Box>
  );
};

export default SearchControl;

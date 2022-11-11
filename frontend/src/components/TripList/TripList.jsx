import React from 'react';
import {
  Radio,
  RadioGroup,
  Button,
  TextField,
  Fab,
  Box,
  Checkbox,
  IconButton,
  MenuItem,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  FormControlLabel
} from '@mui/material';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import DeleteIcon from '@mui/icons-material/Delete';
import EastIcon from '@mui/icons-material/East';
import WestIcon from '@mui/icons-material/West';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import axios from 'axios';

function TripList() {
  const [open, setOpen] = React.useState(false);
  const [isEditingId, setIsEditingId] = React.useState(null);
  const [trips, setTrips] = React.useState([]);
  const [newTrip, setNewTrip] = React.useState({
    LocationStartLon: '',
    LocationStartLat: '',
    LocationEndLon: '',
    LocationEndLat: '',
    address: '',
    lesson: 1,
    toGSO: false,
    sameGender: false,
    weekDay: '',
    availableSeats: 0
  });

  const weekdays = ['Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag'];
  const hours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

  React.useEffect(() => {
    axios.get(`https://localhost:7200/Trips`).then((res) => {
      const response = res.data;
      setTrips(response);
    });
  }, []);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    setTimeout(() => window.location.reload(), 1000);
    c;
  };

  const handleWeekdayChange = (event) => {
    const newTrips = trips.map((trip) =>
      trip.id === isEditingId ? { ...trip, weekDay: event.target.value } : trip
    );
    const newTrip = newTrips.find((obj) => obj.id === isEditingId);
    axios
      .put(`https://localhost:7200/Trip/${isEditingId}`, {
        LocationStartLon: newTrip.locationStart.longitude,
        LocationStartLat: newTrip.locationStart.latitude,
        LocationEndLon: newTrip.locationEnd.longitude,
        LocationEndLat: newTrip.locationEnd.latitude,
        lesson: newTrip.lesson,
        sameGender: newTrip.sameGender,
        availableSeats: newTrip.availableSeats,
        address: newTrip.address,
        weekDay: newTrip.weekDay
      })
      .then((res) => {
        console.log(res);
      });
    setTrips(newTrips);
  };

  const handleLessonChange = (event) => {
    const newTrips = trips.map((trip) =>
      trip.id === isEditingId ? { ...trip, lesson: event.target.value } : trip
    );
    const newTrip = newTrips.find((obj) => obj.id === isEditingId);
    axios
      .put(`https://localhost:7200/Trip/${isEditingId}`, {
        LocationStartLon: newTrip.locationStart.longitude,
        LocationStartLat: newTrip.locationStart.latitude,
        LocationEndLon: newTrip.locationEnd.longitude,
        LocationEndLat: newTrip.locationEnd.latitude,
        lesson: newTrip.lesson,
        sameGender: newTrip.sameGender,
        availableSeats: newTrip.availableSeats,
        address: newTrip.address,
        weekDay: newTrip.weekDay
      })
      .then((res) => {
        console.log(res);
      });
    setTrips(newTrips);
  };

  const handleSameGenderChange = (event) => {
    const newTrips = trips.map((trip) =>
      trip.id === isEditingId ? { ...trip, sameGender: event.target.checked } : trip
    );
    const newTrip = newTrips.find((obj) => obj.id === isEditingId);
    axios
      .put(`https://localhost:7200/Trip/${isEditingId}`, {
        LocationStartLon: newTrip.locationStart.longitude,
        LocationStartLat: newTrip.locationStart.latitude,
        LocationEndLon: newTrip.locationEnd.longitude,
        LocationEndLat: newTrip.locationEnd.latitude,
        lesson: newTrip.lesson,
        sameGender: newTrip.sameGender,
        availableSeats: newTrip.availableSeats,
        address: newTrip.address,
        weekDay: newTrip.weekDay
      })
      .then((res) => {
        console.log(res);
      });
    setTrips(newTrips);
  };

  const handleAddressChange = (event) => {
    const access_token =
      'pk.eyJ1IjoiZmFkZTEzMDkiLCJhIjoiY2xhOWVkbTh5MGRpMTNxcW9oeWN5NGxoYyJ9.POjmItIyWziDT51NqsYohg';

    const newTrips = trips.map((trip) =>
      trip.id === isEditingId ? { ...trip, address: event.target.value } : trip
    );
    const newTrip = newTrips.find((obj) => obj.id === isEditingId);
    axios
      .get(
        `https://api.mapbox.com/geocoding/v5/mapbox.places/${newTrip.address}.json?country=de&limit=3&proximity=ip&types=place&language=de&access_token=${access_token}`
      )
      .then((response) => {
        const address = response.data.features[0].center;
        console.log(address);
        setNewTrip({
          ...newTrip,
          LocationStartLon: address[0],
          LocationStartLat: address[1],
          LocationEndLon: address[0],
          LocationEndLat: address[1]
        });
        console.log(newTrip);
        axios
          .put(`https://localhost:7200/Trip/${isEditingId}`, {
            LocationStartLon: newTrip.locationStart.longitude,
            LocationStartLat: newTrip.locationStart.latitude,
            LocationEndLon: newTrip.locationEnd.longitude,
            LocationEndLat: newTrip.locationEnd.latitude,
            Lesson: newTrip.lesson,
            SameGender: newTrip.sameGender,
            AvailableSeats: newTrip.availableSeats,
            Address: newTrip.address,
            WeekDay: newTrip.weekDay
          })
          .then((res) => {
            console.log(res);
          });
        setTrips(newTrips);
      });
  };

  const handleDelete = (tripId) => {
    const newTrips = trips.filter((item) => item.id !== tripId);

    setTrips(newTrips);

    axios.delete(`https://localhost:7200/Trip/${tripId}`);
  };

  const handleEdit = (tripId) => {
    if (isEditingId === tripId) {
      setIsEditingId(null);
    } else {
      setIsEditingId(tripId);
    }
  };

  const handleAdd = () => {
    const access_token =
      'pk.eyJ1IjoiZmFkZTEzMDkiLCJhIjoiY2xhOWVkbTh5MGRpMTNxcW9oeWN5NGxoYyJ9.POjmItIyWziDT51NqsYohg';

    axios
      .get(
        `https://api.mapbox.com/geocoding/v5/mapbox.places/${newTrip.address}.json?country=de&limit=3&proximity=ip&types=place&language=de&access_token=${access_token}`
      )
      .then((response) => {
        const address = response.data.features[0].center;
        console.log(response);
        const tripToAdd = {
          ...newTrip,
          LocationStartLon: address[0],
          LocationStartLat: address[1],
          LocationEndLon: address[0],
          LocationEndLat: address[1]
        };
        console.log(tripToAdd);
        axios
          .post('https://localhost:7200/Trip', {
            DriverId: 1,
            LocationStartLat: String(tripToAdd.LocationStartLat),
            LocationStartLon: String(tripToAdd.LocationStartLon),
            LocationEndLon: String(tripToAdd.LocationEndLon),
            LocationEndLat: String(tripToAdd.LocationEndLat),
            d: String(tripToAdd.LocationEndLon),
            lesson: tripToAdd.lesson,
            sameGender: tripToAdd.sameGender,
            availableSeats: tripToAdd.availableSeats,
            address: tripToAdd.address,
            weekDay: tripToAdd.weekDay
          })
          .then((response) => {
            console.log(response);
          });
      });
    handleClose();
  };

  return (
    trips && (
      <Box>
        <TableContainer width="100vw">
          <Table stickyHeader aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell sx={{ fontWeight: 'bold' }}>Adresse</TableCell>
                <TableCell sx={{ fontWeight: 'bold' }}>Tag</TableCell>
                <TableCell sx={{ fontWeight: 'bold' }}>Schulstunde</TableCell>
                <TableCell sx={{ fontWeight: 'bold' }}>Gleiches Geschlecht</TableCell>
                <TableCell sx={{ fontWeight: 'bold' }}>Hinfahrt / Rückfahrt</TableCell>
                <TableCell align="right" />
              </TableRow>
            </TableHead>
            <TableBody>
              {trips.map((trip) => {
                return isEditingId !== trip.id ? (
                  <TableRow
                    key={trip.id}
                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                    <TableCell>{trip.address}</TableCell>
                    <TableCell>{trip.weekDay}</TableCell>
                    <TableCell>{trip.lesson}</TableCell>
                    <TableCell>
                      <Checkbox
                        sx={{ ml: 5 }}
                        align="center"
                        disabled={isEditingId === trip.id ? false : true}
                        defaultChecked={trip.sameGender}
                      />
                    </TableCell>
                    <TableCell>
                      {trip.toGSO ? <EastIcon sx={{ ml: 6 }} /> : <WestIcon sx={{ ml: 6 }} />}
                    </TableCell>
                    <TableCell align="right">
                      <IconButton onClick={() => handleEdit(trip.id)}>
                        <EditIcon />
                      </IconButton>
                      <IconButton onClick={() => handleDelete(trip.id)}>
                        <DeleteIcon />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                ) : (
                  <TableRow
                    key={trip.id}
                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                    <TableCell>
                      <TextField
                        multiline
                        defaultValue={trip.address}
                        fullWidth
                        id="address"
                        variant="standard"
                        onChange={handleAddressChange}
                      />
                    </TableCell>
                    <TableCell>
                      <TextField
                        id="weekdays"
                        select
                        variant="standard"
                        name="weekdays"
                        fullWidth
                        defaultValue={trip.weekDay}
                        onChange={handleWeekdayChange}>
                        {weekdays.map((option) => (
                          <MenuItem key={option} value={option}>
                            {option}
                          </MenuItem>
                        ))}
                      </TextField>
                    </TableCell>
                    <TableCell>
                      <TextField
                        id="startHour"
                        select
                        variant="standard"
                        name="startHour"
                        fullWidth
                        defaultValue={trip.lesson}
                        onChange={handleLessonChange}>
                        {hours.map((option) => (
                          <MenuItem key={option} value={option}>
                            {option}
                          </MenuItem>
                        ))}
                      </TextField>
                    </TableCell>
                    <TableCell>
                      <Checkbox
                        id="sameGender"
                        sx={{ ml: 5 }}
                        align="center"
                        disabled={isEditingId === trip.id ? false : true}
                        defaultChecked={trip.sameGender}
                        onChange={handleSameGenderChange}
                      />
                    </TableCell>
                    <TableCell>
                      {trip.toGSO ? <EastIcon sx={{ ml: 6 }} /> : <WestIcon sx={{ ml: 6 }} />}
                    </TableCell>
                    <TableCell align="right">
                      <IconButton onClick={() => handleEdit(trip.id)}>
                        <EditIcon color={isEditingId === trip.id && 'primary'} />
                      </IconButton>
                      <IconButton onClick={() => handleDelete(trip.id)}>
                        <DeleteIcon />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
        <Fab
          sx={{ m: 4, left: 10, bottom: 4, position: 'fixed', mr: 2 }}
          color="primary"
          aria-label="add"
          onClick={handleClickOpen}>
          <AddIcon />
        </Fab>
        <Dialog open={open} onClose={handleClose}>
          <DialogTitle>Fahrt anlegen</DialogTitle>
          <DialogContent>
            <DialogContentText>
              Geben Sie bitte die folgenden Daten ein um eine neue Fahrt anzulegen
            </DialogContentText>
            <TextField
              sx={{ m: 1, mb: 3, width: '93%' }}
              autoFocus
              margin="dense"
              id="address"
              label="Adresse (Straße, Hausnr., PLZ, Ort)"
              type="address"
              variant="outlined"
              value={newTrip.address}
              onChange={(event) => setNewTrip({ ...newTrip, address: event.target.value })}
            />
            <TextField
              sx={{ width: '30%', m: 1 }}
              id="weekday"
              select
              label="Wochentag"
              name="weekday"
              value={newTrip.weekDay}
              onChange={(event) => setNewTrip({ ...newTrip, weekDay: event.target.value })}>
              {weekdays.map((weekday) => (
                <MenuItem key={weekday} value={weekday}>
                  {weekday}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              sx={{ width: '28%', m: 1 }}
              id="lesson"
              select
              label="Schulstunde"
              name="lesson"
              value={newTrip.lesson}
              onChange={(event) => setNewTrip({ ...newTrip, lesson: event.target.value })}>
              {hours.map((hour) => (
                <MenuItem key={hour} value={hour}>
                  {hour}
                </MenuItem>
              ))}
            </TextField>
            <FormControlLabel
              labelPlacement="top"
              control={
                <Checkbox
                  checked={newTrip.sameGender}
                  onChange={(event) => setNewTrip({ ...newTrip, sameGender: event.target.checked })}
                />
              }
              label="Gleiches Geschlecht"
            />
            <RadioGroup
              sx={{ m: 1 }}
              value={newTrip.toGSO === true ? 'Hinfahrt' : 'Rückfahrt'}
              onChange={(event) =>
                setNewTrip({ ...newTrip, toGSO: event.target.value === 'Hinfahrt' ? true : false })
              }
              aria-labelledby="ride-type"
              name="ride-type">
              <FormControlLabel value="Hinfahrt" control={<Radio />} label="Hinfahrt" />
              <FormControlLabel value="Rückfahrt" control={<Radio />} label="Rückfahrt" />
            </RadioGroup>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose}>Abbrechen</Button>
            <Button onClick={handleAdd}>Speichern</Button>
          </DialogActions>
        </Dialog>
      </Box>
    )
  );
}

export default TripList;

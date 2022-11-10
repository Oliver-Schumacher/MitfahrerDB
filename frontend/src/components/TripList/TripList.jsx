import React from 'react';
import {
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
  TableRow
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EastIcon from '@mui/icons-material/East';
import WestIcon from '@mui/icons-material/West';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import SaveIcon from '@mui/icons-material/Save';
import axios from 'axios';

function TripList() {
  const [isEditingId, setIsEditingId] = React.useState(null);
  const [trips, setTrips] = React.useState([]);

  const weekdays = ['Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag'];
  const hours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

  React.useEffect(() => {
    axios.get(`https://localhost:7200/Trips`).then((res) => {
      const response = res.data;
      setTrips(response);
    });
  }, []);

  const handleWeekdayChange = (event) => {
    const newTrips = trips.map((trip) =>
      trip.id === isEditingId ? { ...trip, weekDay: event.target.value } : trip
    );
    const newTrip = newTrips.find((obj) => obj.id === isEditingId);
    axios
      .put(`https://localhost:7200/Trip/${isEditingId}`, {
        locStartLong: newTrip.locationStart.longitude,
        locStartLat: newTrip.locationStart.latitude,
        locEndLong: newTrip.locationEnd.longitude,
        locEndLat: newTrip.locationEnd.latitude,
        startTime: newTrip.startTime,
        sameGender: newTrip.sameGender,
        availableSeats: newTrip.availableSeats,
        adress: newTrip.adress
      })
      .then((res) => {
        console.log(res);
      });
  };

  const handleSameGenderChange = (event) => {
    const newTrips = trips.map((trip) =>
      trip.id === isEditingId ? { ...trip, sameGender: event.target.value } : trip
    );
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

  const handleAdd = () => {};

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
                    <TableCell>{trip.adress}</TableCell>
                    <TableCell>{trip.weekDay}</TableCell>
                    <TableCell>{trip.startTime}</TableCell>
                    <TableCell>
                      <Checkbox
                        sx={{ ml: 5 }}
                        align="center"
                        disabled={isEditingId === trip.id ? false : true}
                        defaultChecked={trip.sameGender}
                      />
                    </TableCell>
                    <TableCell>
                      {false ? <EastIcon sx={{ ml: 6 }} /> : <WestIcon sx={{ ml: 6 }} />}
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
                        defaultValue={trip.adress}
                        fullWidth
                        id="address"
                        variant="standard"
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
                        defaultValue={trip.startTime}
                        onChange={handleWeekdayChange}>
                        {hours.map((option) => (
                          <MenuItem key={option} value={option}>
                            {option}
                          </MenuItem>
                        ))}
                      </TextField>
                    </TableCell>
                    <TableCell>
                      <Checkbox
                        id={'sameGender'}
                        sx={{ ml: 5 }}
                        align="center"
                        disabled={isEditingId === trip.id ? false : true}
                        defaultChecked={trip.sameGender}
                      />
                    </TableCell>
                    <TableCell>
                      {false ? <EastIcon sx={{ ml: 6 }} /> : <WestIcon sx={{ ml: 6 }} />}
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
          sx={{ m: 4, right: 10, bottom: 4, position: 'fixed', mr: 2 }}
          color="primary"
          aria-label="add">
          <AddIcon />
        </Fab>
      </Box>
    )
  );
}

export default TripList;

import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { useNavigate, Link } from 'react-router-dom';
import axios from 'axios';

const theme = createTheme();

const genders = [
  {
    value: '1',
    label: 'Männlich'
  },
  {
    value: '2',
    label: 'Weiblich'
  },
  {
    value: '3',
    label: 'Divers'
  }
];

export default function Register() {
  const [gender, setGender] = React.useState('');
  const navigate = useNavigate();

  const handleChange = (event) => {
    setGender(event.target.value);
    console.log(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    axios
      .post('https://localhost:7200/User/Registration', {
        Name: data.get('username'),
        Mail: data.get('email'),
        Passwort: data.get('password'),
        Phone: data.get('mobile'),
        GenderId: data.get('gender')
      })
      .then((res) => {
        navigate('/login');
        console.log(res);
      });
  };

  return (
    <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 4,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center'
          }}>
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Registrieren
          </Typography>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField required fullWidth id="username" label="Username" name="username" />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="E-Mail-Adresse"
                  name="email"
                  autoComplete="email"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Passwort"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField fullWidth id="mobile" label="Handynummer" name="mobile" />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  id="gender"
                  select
                  label="Geschlecht"
                  name="gender"
                  fullWidth
                  helperText="Bitte geben sie ihr Geschlecht ein"
                  value={gender}
                  onChange={handleChange}>
                  {genders.map((option) => (
                    <MenuItem key={option.value} value={option.value}>
                      {option.label}
                    </MenuItem>
                  ))}
                </TextField>
              </Grid>
            </Grid>
            <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }}>
              Registrieren
            </Button>
            <Grid container justifyContent="center">
              <Grid item>
                <Link to="/login">Du hast schon einen Account? Login</Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}

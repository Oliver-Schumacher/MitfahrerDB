import axios from 'axios';

const setAuthToken = (token) => {
  if (token) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else delete axios.defaults.headers.common['Authorization'];
};

export async function handleLogin({ _email, _password }) {
  axios
    .post('https://localhost:7200/User/Login', {
      mail: _email,
      passwort: _password
    })
    .then((response) => {
      //get token from response
      const token = 'H1312MASKGKXXLK955GHUasdjnJ8KK';
      //set JWT token to local
      localStorage.setItem('token', token);

      //set token to axios common header
      setAuthToken(token);
    })
    .catch((err) => console.log(err));
}

export const handleLogout = () => {
  localStorage.removeItem('token');
};

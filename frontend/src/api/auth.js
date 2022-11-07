import axios from 'axios';

const setAuthToken = (token) => {
  if (token) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else delete axios.defaults.headers.common['Authorization'];
};

export const handleLogin = ({ _email, _password }) => {
  //reqres registered sample user
  const loginPayload = {
    email: _email,
    password: _password
  };

  axios
    .post('https://reqres.in/api/login', loginPayload)
    .then((response) => {
      //get token from response
      const token = response.data.token;

      //set JWT token to local
      localStorage.setItem('token', token);

      //set token to axios common header
      setAuthToken(token);
    })
    .catch((err) => console.log(err));
};

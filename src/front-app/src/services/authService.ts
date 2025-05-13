import axios from "axios";

const API_URL = "http://localhost:8080/api/auth";

export const register = async (
  userName: string,
  email: string,
  password: string
) => {
  await axios.post(`${API_URL}/register`, {
    userName,
    email,
    password,
  });
};

export const login = async (email: string, password: string) => {
  const response = await axios.post(`${API_URL}/login`, { email, password });
  return response.data;
};

export const refreshToken = async (
  accessToken: string,
  refreshToken: string
) => {
  const response = await axios.post(`${API_URL}/refresh`, {
    accessToken,
    refreshToken,
  });
  return response.data;
};

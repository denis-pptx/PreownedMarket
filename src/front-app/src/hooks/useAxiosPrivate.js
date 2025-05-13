import axios from "axios";
import { useEffect } from "react";
import { useAuth } from "./useAuth";
import { useRefreshToken } from "./useRefreshToken";

export const useAxiosPrivate = () => {
  const { auth } = useAuth();
  const refresh = useRefreshToken();

  // Создаём экземпляр axios
  const axiosPrivate = axios.create({
    baseURL: "http://localhost:8080",
    headers: { "Content-Type": "application/json" },
  });

  useEffect(() => {
    // Добавляем интерсептор для добавления accessToken в заголовки
    const requestIntercept = axiosPrivate.interceptors.request.use(
      (config) => {
        const token = localStorage.getItem("accessToken");
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
      },
      (error) => Promise.reject(error)
    );

    // Добавляем интерсептор для обработки ответов (401 -> обновление токена)
    const responseIntercept = axiosPrivate.interceptors.response.use(
      (response) => response,
      async (error) => {
        const prevRequest = error.config;
        if (error.response?.status === 401 && !prevRequest._retry) {
          prevRequest._retry = true;
          try {
            const newAccessToken = await refresh();
            // Повторяем запрос с новым токеном
            prevRequest.headers.Authorization = `Bearer ${newAccessToken}`;
            return axiosPrivate(prevRequest);
          } catch (err) {
            // Если обновление не удалось, очистим состояние
            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");
            throw err;
          }
        }
        return Promise.reject(error);
      }
    );

    return () => {
      axiosPrivate.interceptors.request.eject(requestIntercept);
      axiosPrivate.interceptors.response.eject(responseIntercept);
    };
  }, [auth, refresh]);

  return axiosPrivate;
};

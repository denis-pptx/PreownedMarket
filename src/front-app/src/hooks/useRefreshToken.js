import { useAuth } from "./useAuth";
import { refreshToken as refreshTokenAPI } from "../services/authService";
import { jwtDecode } from "jwt-decode";

export const useRefreshToken = () => {
  const { setAuth } = useAuth();

  const refresh = async () => {
    const storedAccessToken = localStorage.getItem("accessToken");
    const storedRefreshToken = localStorage.getItem("refreshToken");

    if (!storedToken || !storedRefreshToken) throw new Error("Нет токенов");

    try {
      const data = await refreshTokenAPI(storedAccessToken, storedRefreshToken);

      localStorage.setItem("accessToken", data.accessToken);
      localStorage.setItem("refreshToken", data.refreshToken);

      const decoded = jwtDecode(token);
      setAuth({
        userId:
          decoded[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
          ],
        userName:
          decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        role: decoded[
          "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        ],
      });
      return data.accessToken;
    } catch (err) {
      console.error("Не удалось обновить токен", err);
      throw err;
    }
  };

  return refresh;
};

import React, { createContext, useContext, useState, useEffect } from "react";
import { decodeToken, type DecodedToken } from "../utils/decodeToken";

interface AuthContextProps {
  auth: DecodedToken | null;
  setAuth: (token: string) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [auth, setAuthState] = useState<DecodedToken | null>(null);

  const setAuth = (token: string) => {
    const decoded = decodeToken(token);
    setAuthState(decoded);
    localStorage.setItem("accessToken", token);
  };

  const logout = () => {
    setAuthState(null);
    localStorage.removeItem("accessToken");
  };

  useEffect(() => {
    const token = localStorage.getItem("accessToken");
    if (token) {
      setAuth(token);
    }
  }, []);

  return (
    <AuthContext.Provider value={{ auth, setAuth, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};

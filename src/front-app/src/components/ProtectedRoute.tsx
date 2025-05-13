import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

interface ProtectedRouteProps {
  requiredRoles: string[];
  redirectPath?: string;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({
  requiredRoles,
  redirectPath = "/",
}) => {
  const { auth } = useAuth();

  if (!auth) {
    // Если пользователь не авторизован
    return <Navigate to="/login" replace />;
  }

  if (!requiredRoles.includes(auth.role)) {
    // Если роль не соответствует требуемой, перенаправляем
    return <Navigate to={redirectPath} replace />;
  }

  // Если всё нормально — рендерим дочерние компоненты
  return <Outlet />;
};

export default ProtectedRoute;

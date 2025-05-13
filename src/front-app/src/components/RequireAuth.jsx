import { useLocation, Navigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";

export const RequireAuth = ({ children, allowedRoles }) => {
  const { auth } = useAuth();
  const location = useLocation();

  // Если пользователь не аутентифицирован, редирект на логин
  if (!auth.userId) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // Если указаны allowedRoles и ни одна не совпадает с ролями пользователя
  if (
    allowedRoles?.length &&
    !allowedRoles.some((role) => auth.roles.includes(role))
  ) {
    return <Navigate to="/unauthorized" replace />;
  }

  // Разрешаем доступ
  return children;
};

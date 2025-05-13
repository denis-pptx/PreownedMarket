import { jwtDecode } from "jwt-decode";

export interface DecodedToken {
  userId: string;
  userName: string;
  role: string;
}

export const decodeToken = (token: string): DecodedToken => {
  const decoded: any = jwtDecode(token);
  return {
    userId:
      decoded[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
      ],
    userName:
      decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
    role: decoded[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ],
  };
};

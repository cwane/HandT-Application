import { useLocation, Outlet, Navigate } from "react-router-dom";
import { useAuthState } from '../../context/context'

const RequireAuthentication = ({ allowedRoles }) => {
    const location = useLocation();
    const authentication = useAuthState();
    console.log("authentication", authentication);

  return (
    authentication?.roles?.find(role => allowedRoles?.includes(role))
        ? <Outlet />
        : (authentication?.token
            ? <Navigate to="/unauthorized" state={{ from: location }} replace />
        : <Navigate to="/login-page" state={{ from: location }} replace />)
  )
}

export default RequireAuthentication
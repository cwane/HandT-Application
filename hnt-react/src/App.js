import './App.css';
import { Routes, Route } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';

import Header from './components/Header/Header';
import Footer from './components/Footer/Footer';
import RequireAuthentication from './components/RequireAuthentication/RequireAuthentication';
import HomePage from './pages/HomePage/HomePage';
import LoginPage from './pages/LoginPage/LoginPage';
import RegistrationPage from './pages/RegistrationPage/RegistrationPage';
import ProfileSetupPage from './pages/ProfileSetupPage/ProfileSetupPage';
import SingleEventPage from './pages/SingleEventPage/SingleEventPage';
import EventListingPage from './pages/EventListingPage/EventListingPage';
import Profilepage from './pages/ProfilePage/Profilepage';
import ResetPasswordPage from './pages/ResetPasswordPage/ResetPasswordPage';
import ForgotPasswordPage from './pages/ForgotPasswordPage/ForgotPasswordPage';
import ChangePasswordPage from './pages/ChangePasswordPage/ChangePasswordPage';
import VerifyUser from './pages/VerifyUser/VerifyUser';
import AdminDashboardPage from './pages/AdminDashboardPage/AdminDashboardPage';
import CreateInternalUserPage from './pages/CreateInternalUserPage/CreateInternalUserPage';
import CreateEventPage from './pages/CreateEventPage/CreateEventPage';


function App() {
  return (
    <div className="hfeed site" id="page">

      <Header />
      
      <div id="content" className="site-content">
        <div id="primary" className="content-area">
          <main id="main" className="site-main">

            <Routes>
              <Route path="/">
                {/* public routes */}
                <Route path="/" exact element={<HomePage />} />
                <Route path="login-page" element={<LoginPage />} />
                <Route path="signup-page" element={<RegistrationPage />} />
                <Route path="event-listing" element={<EventListingPage />} />
                <Route path="single-event" element={<SingleEventPage />} />
                <Route path="verify/:token" element={<VerifyUser />} />
                <Route path="create-event" element={<CreateEventPage />}   />

                {/* we want to protect these routes */}
                <Route element={<RequireAuthentication allowedRoles={["Admin", "Internal", "External"]}/>} >
                  <Route path="profile-page" element={<Profilepage />} />
                  <Route path="profile-setup" element={<ProfileSetupPage />} />
                  <Route path="reset-password/:token" element={<ResetPasswordPage />} />
                  <Route path ="forgot-password" element={<ForgotPasswordPage />} />
                  <Route path="change-password" element={<ChangePasswordPage />} />
                </Route>

                {/* admin protected routes */}
                <Route element={<RequireAuthentication allowedRoles={["Admin"]}/>} >
                  <Route path="admin-dashboard" element={<AdminDashboardPage />}>
                    <Route path="create-internal-user" element={<CreateInternalUserPage />} />
                  </Route>
                </Route>

                {/* catch all page */}
              </Route>
            </Routes>

          </main>
          {/* #main */}
        </div>
        {/* #primary */}
      </div>
      {/* .site-content */}

      <ToastContainer />

      <Footer />
    </div>
  );
}

export default App;

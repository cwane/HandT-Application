import React from 'react';
import './RegistrationPage.css'

import axios from 'axios'
import { useForm as useHookForm } from 'react-hook-form'
import { useAuthDispatch } from '../../context/context';
import { useNavigate } from 'react-router-dom';
import { toastSettings } from '../../utils/toastSettings';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import LoginLogo from '../../assets/images/login-logo.png'
import SignupMainImage from '../../assets/images/signup-main-image.png'
import RegistrationForm from '../../components/RegistrationForm/RegistrationForm'


const RegistrationPage = () => {

  const methods = useHookForm();
  const dispatch = useAuthDispatch();
  const navigate = useNavigate();

  const handleSubmit = methods.handleSubmit( async (data) => {
    let payload = { fullname: data.fullname, username: data.username, email: data.email, password: data.password };

    try {
        dispatch({ type: 'REQUEST_LOGIN' });
        const response = await axios.post(`${process.env.REACT_APP_ROOT_URL}/Authentication/register`, payload);

        if (response.data.data.token) {
          dispatch({ type: 'LOGIN_SUCCESS', payload: response.data.data });
          localStorage.setItem('currentUser', JSON.stringify(response.data.data));
          toast.success('User registered successfully!', toastSettings);
          navigate('/profile-setup');
          return;
        }
    } catch (error) {
      dispatch({ type: 'LOGIN_ERROR', error: error });
      toast.error(error.response.data.responseMessage, toastSettings);
    }
  });

return (
  <>
    <section class="login-page-wrap">
      <div class="container">
        <div class="login-content-wrap signup-content-wrap">
          <div class="login-header">
            <div class="login-logo">
              <a href="#">
                <img src= { LoginLogo } alt="" />
              </a>
            </div>
            <h2 class="entry-title">Get Started With H&T</h2>
            <span style={{color: '#808080'}}>Getting started is easy</span>
          </div>
                  
                  <RegistrationForm handleSubmit={handleSubmit} methods={methods} />
                  
          </div>
          </div>
          <div class="login-image-wrap">
              <img src={SignupMainImage} alt="" />
          </div>
    </section>
  </>
  )
}


export default RegistrationPage;

import React from 'react'
import './LoginPage.css'
import { useForm as useHookForm } from 'react-hook-form'
import { loginUser } from '../../context/actions'
import { useAuthDispatch, useAuthState } from '../../context/context'
import { toastSettings } from '../../utils/toastSettings'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useNavigate, useLocation } from 'react-router-dom'

import LoginLogo from '../../assets/images/login-logo.png'
import LoginMainImage from '../../assets/images/login-main-image.png'
import LoginForm from '../../components/LoginForm/LoginForm'


const LoginPage = () => {
    const methods = useHookForm();
    const dispatch = useAuthDispatch();
    const { errorMessage } = useAuthState();
    const navigate = useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || '/';

    const handleSubmit = methods.handleSubmit(async (data) => {
        let payload = { username: data.username, password: data.password };

        try {
            let response = await loginUser(dispatch, payload);
            if (!response.token) {
                return;
            };  
            toast.success('Login Successful', toastSettings);
            navigate(from, { replace: true });
        } catch (error) {
            toast.error(errorMessage, toastSettings);
        }
    });

  return (
    <section class="login-page-wrap">
        <div class="container">
            <div class="login-content-wrap">
                <div class="login-header">
                    <div class="login-logo">
                        <a href="#">
                            <img src={LoginLogo} alt="" />
                        </a>
                    </div>
                    <h2 class="entry-title">Welcome Back</h2>
                    <span>Login into your account</span>
                </div>
                
                <LoginForm handleSubmit={handleSubmit} methods={methods} />

            </div>
        </div>
        <div class="login-image-wrap">
            <img src={LoginMainImage} alt="" />
        </div>
    </section>
  )
}

export default LoginPage
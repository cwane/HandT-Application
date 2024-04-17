import React from 'react';
import './ResetPasswordPage.css'
import axios from 'axios';
import { useForm as useHookForm } from 'react-hook-form'
import { toastSettings } from '../../utils/toastSettings'
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import SetNewPassword from '../../components/SetNewPassword/SetNewPassword';
import LoginMainImage from '../../assets/images/login-main-image.png'

const ResetPasswordPage = () => {
  const { token } = useParams();
  const navigate = useNavigate();
  const methods = useHookForm();
  const [loading, setLoading] = React.useState(false);

  const handleSubmit = methods.handleSubmit(async (data) => {
    let payload = { token: token, password: data.password };

    setLoading(true);
    axios.post(`${process.env.REACT_APP_ROOT_URL}/Authentication/reset-password`, payload)
    .then(response => {
      setLoading(false);
      toast.success('Password successfully reset!', toastSettings);
      navigate('/login');
    })
    .catch(error => {
      setLoading(false);
      toast.error(error.message, toastSettings);
    });
  });
  
  return (
  
    <section class="login-page-wrap">
        <div class="container">
            <div class="login-content-wrap">
                 
                <SetNewPassword handleSubmit={handleSubmit} methods={methods} loading={loading} />

            </div>
        </div>
        <div class="login-image-wrap">
            <img src={LoginMainImage} alt="" />
        </div>
    </section>


  );
}

export default ResetPasswordPage;

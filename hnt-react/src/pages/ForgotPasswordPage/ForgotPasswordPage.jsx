import React from 'react';
import './ForgotPasswordPage.css'
import { useForm as useHookForm } from 'react-hook-form'
import { toastSettings } from '../../utils/toastSettings'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import ForgotPassword from '../../components/ForgotPassword/ForgotPassword';
import LoginMainImage from '../../assets/images/login-main-image.png'
import axios from 'axios';

const ForgotPasswordPage = () => {
  const methods = useHookForm();
  const [loading, setLoading] = React.useState(false);

  const handleSubmit = methods.handleSubmit(async (data) => {
    let payload = { email: data.email };

    setLoading(true);
    axios.post(`${process.env.REACT_APP_ROOT_URL}/Authentication/forgot-password`, payload)
    .then(response => {
      setLoading(false);
      toast.success('Reset token was successfully sent to your email address!', toastSettings);
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
              
                <ForgotPassword handleSubmit={handleSubmit} methods={methods} loading={loading} />

            </div>
        </div>
        <div class="login-image-wrap">
            <img src={LoginMainImage} alt="" />
        </div>
    </section>

  );
}

export default ForgotPasswordPage;

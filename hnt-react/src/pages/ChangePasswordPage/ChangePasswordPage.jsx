import React from 'react';
import LoginMainImage from '../../assets/images/login-main-image.png'
import ChangePassword from '../../components/ChangePassword/ChangePassword';
import './ChangePasswordPage.css'
import { useForm as useHookForm } from 'react-hook-form'
import { toastSettings } from '../../utils/toastSettings'
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';
import { useAuthState } from '../../context/context';

const ChangePasswordPage = () => {
  const authentication = useAuthState();
  const navigate = useNavigate();
  const methods = useHookForm();
  const [loading, setLoading] = React.useState(false);

  const handleSubmit = methods.handleSubmit(async (data) => {
    let payload = { oldpassword: data.oldpassword, newpassword: data.newpassword };

    setLoading(true);

    const config = {
      headers: { Authorization: `Bearer ${authentication.token}` }
    };

    axios.post(`${process.env.REACT_APP_ROOT_URL}/Authentication/change-password`, payload, config)
    .then(response => {
      setLoading(false);
      toast.success('Password successfully changed!', toastSettings);
      navigate('/profile-page');
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
            
              
              <ChangePassword handleSubmit={handleSubmit} methods={methods} loading={loading} />

          </div>
      </div>
      <div class="login-image-wrap">
          <img src={LoginMainImage} alt="" />
      </div>
    </section>
  );
}

export default ChangePasswordPage;

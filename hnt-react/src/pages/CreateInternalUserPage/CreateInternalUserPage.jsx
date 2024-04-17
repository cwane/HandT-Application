import React from 'react'
import CreateInternalUserForm from '../../components/CreateInternalUserForm/CreateInternalUserForm'
import axios from 'axios';
import { useForm as useHookForm } from 'react-hook-form'
import { toastSettings } from '../../utils/toastSettings'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useAuthState } from '../../context/context';

const CreateInternalUserPage = () => {
    const authentication = useAuthState();
    const methods = useHookForm();
    const [loading, setLoading] = React.useState(false);

    const handleSubmit = methods.handleSubmit(async (data) => {
        let payload = { fullname: data.fullname, username: data.username, email: data.email, password: data.password };

        setLoading(true);

        const config = {
        headers: { Authorization: `Bearer ${authentication.token}` }
        };

        axios.post(`${process.env.REACT_APP_ROOT_URL}/Authentication/register-internal-user`, payload, config)
        .then(response => {
            setLoading(false);
            toast.success('Internal User Created successfully!', toastSettings);
        })
        .catch(error => {
            setLoading(false);
            toast.error(error.message, toastSettings);
        });
    });
  return (
    <div class="container">
          <div class="login-content-wrap">
            
              <CreateInternalUserForm handleSubmit={handleSubmit} methods={methods} loading={loading} />

          </div>
    </div>
  )
}

export default CreateInternalUserPage
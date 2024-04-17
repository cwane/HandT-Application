import axios from 'axios';
import React, { useEffect } from 'react'
import { useParams } from 'react-router-dom';
import { toastSettings } from '../../utils/toastSettings'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const VerifyUser = () => {
    const { token } = useParams();
    const [loading, setLoading] = React.useState(false);

    useEffect(() => {
        setLoading(true);
        axios.post(`${process.env.REACT_APP_ROOT_URL}/Authentication/verify/${token}`)
        .then(response => {
            setLoading(false);
            toast.success('User successfully verified!', toastSettings);
        })
        .catch(error => {
            setLoading(false);
            toast.error(error.message, toastSettings);
        });
    }, [token])
    
  return (
    <div>VerifyUser</div>
  )
}

export default VerifyUser
import React from 'react';
import { IoArrowBackOutline } from 'react-icons/io5'
import { Link} from 'react-router-dom'
import styled from 'styled-components';
import { FormProvider } from 'react-hook-form';
import { Input } from '../Input/Input';
import LoadingButton from '../LoadingButton/LoadingButton';


export const BackButton = styled.p`
    font-weight: bold;
    font-size: 0.9rem;
    
    &>a {
        display: flex;
        align-items: center;
        gap: 8px;
        cursor: pointer;
        color: #6e6e70;
        text-decoration: none;
    }
`

const ForgotPassword = ({ methods, handleSubmit, loading }) => {
  return (
    <div class="login-form-wrap">
      <h1>Forgot Password?</h1>
      <p>No worries, we'll send you reset instructions.</p>

      <FormProvider {...methods}>
        <form onSubmit={e => e.preventDefault()} noValidate>
          <p>
              {/* <input type="text" name="email" required="" placeholder="Email" value={values.email} onChange={handleChange} />
              <label>Email</label> */}
              <Input
                name="email"
                label="email"
                type="email"
                id="email"
                placeholder="Email"
                validation={{
                    required: {
                        value: true,
                        message: 'email field is required' 
                    },
                    pattern: {
                      value: /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/,
                      message: 'enter a valid email address'
                    }
                }}
              />
          </p>

          <p>
            {/* <input value="Reset Password"  type="submit" /> */}
            { loading ? <LoadingButton /> : <input value="Reset" onClick={handleSubmit} type="submit" /> }
          </p>
        </form>
      </FormProvider>
    <BackButton><Link to="/login-page"><IoArrowBackOutline />Back to log in</Link></BackButton>
  </div>
  );
}

export default ForgotPassword;

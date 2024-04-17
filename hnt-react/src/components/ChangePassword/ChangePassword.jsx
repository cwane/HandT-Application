import React from 'react';
import { FormProvider } from 'react-hook-form';
import { IoArrowBackOutline } from 'react-icons/io5'
import { Link} from 'react-router-dom'
import styled from 'styled-components';
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
const ChangePassword = ({ loading, handleSubmit, methods }) => {
  return (
    <div class="login-form-wrap">
    <h1>Set New Password</h1>
    <p>Your new password must be different to previously used password</p>
    <FormProvider {...methods}>
      <form onSubmit={e => e.preventDefault()} noValidate>
        <p>
          {/* <input type="password" name="password" required="" placeholder="Password" value={values.password} onChange={handleChange} />
          <label>password</label> */}
          <Input
            name="oldpassword"
            label="current password"
            type="password"
            id="oldpassword"
            placeholder="Current Password"
            validation={{
                required: {
                    value: true,
                    message: 'password field is required' 
                },
                minLength: {
                    value: 8,
                    message: "password must have at least 8 characters"
                },
                pattern: {
                    value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
                    message: "password must contain at least one uppercase, one lowercase, one number and one special character"
                }
            }}
          />
          <span class="show-password-input"></span>
        </p>
        
        <p>
          {/* <input type="password" name="password" required="" placeholder="Password" value={values.password} onChange={handleChange} />
          <label>password</label> */}
          <Input
            name="newpassword"
            label="new password"
            type="password"
            id="newpassword"
            placeholder="New Password"
            validation={{
                required: {
                    value: true,
                    message: 'password field is required' 
                },
                minLength: {
                    value: 8,
                    message: "password must have at least 8 characters"
                },
                pattern: {
                    value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
                    message: "password must contain at least one uppercase, one lowercase, one number and one special character"
                }
            }}
          />
          <span class="show-password-input"></span>
        </p>

        <p>
          {/* <input type="password" name="password" required="" placeholder="Password" value={values.password} onChange={handleChange} />
          <label>password</label> */}
          <Input
            name="confirmpassword"
            label="Confirm password"
            type="password"
            id="confirmpassword"
            placeholder="Confirm Password"
            validation={{
                required: {
                    value: true,
                    message: 'password field is required' 
                },
                minLength: {
                    value: 8,
                    message: "password must have at least 8 characters"
                },
                pattern: {
                    value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
                    message: "password must contain at least one uppercase, one lowercase, one number and one special character"
                },
                validate: (value) => value === methods.watch('newpassword') || "passwords don't match"
            }}
          />
          <span class="show-password-input"></span>
        </p>

        <p>
          {loading ? <LoadingButton /> : <input value="Reset Password" onClick={handleSubmit} type="submit" /> }
        </p>
      </form>
    </FormProvider>
    
    <BackButton><Link to="/login-page"><IoArrowBackOutline />Back to log in</Link></BackButton>
</div>

  );
}

export default ChangePassword;

import React, {useState} from 'react';
import { FormProvider } from 'react-hook-form';
import { useAuthState } from '../../context/context';
import { Input } from '../Input/Input';
import ReCAPTCHA from "react-google-recaptcha";

import LoginLink1 from '../../assets/images/login-link1.png'
import LoginLink2 from '../../assets/images/login-link2.png'
import LoadingButton from './../LoadingButton/LoadingButton';

const RegistrationForm = ({ methods, handleSubmit }) => {
  const [verified, setVerified] = useState(false)
  const { loading, errorMessage } = useAuthState();
  
  // Recaptcha function
  function onChange(value) {
      console.log("Captcha value:", value);
      setVerified(true);
    }

  return (
    <div class="login-form-content">
      <div class="login-link-wrap">
        <ul>
          <li>
            <a href="#">
              <img src={LoginLink1} alt="" />
              facebook
            </a>
          </li>
          <li>
            <a href="#">
              <img src={LoginLink2} alt="" />
              google
            </a>
          </li>
        </ul>
      </div>
      <div class="login-devider">
        <span>Or continue with</span>
      </div>
      <div class="login-form-wrap">
        <FormProvider {...methods}>
          <form onSubmit={e => e.preventDefault()} noValidate>
          
            <p>
              {/* <input type="text" name="fullname" required="" placeholder="Fullname" value={values.fullname} onChange={handleChange} />
              <label>Fullname</label> */}
              <Input
                name="fullname"
                label="fullname"
                type="text"
                id="fullname"
                placeholder="Fullname"
                validation={{
                    required: {
                        value: true,
                        message: 'fullname field is required' 
                    },
                    pattern: {
                      value: /^[a-zA-Z]+ [a-zA-Z]+$/,
                      message: 'fullname must contain only alphabetical characters and spaces'
                    }
                }}
              />
            </p>
            
            <p>
              {/* <input type="text" name="username" required="" placeholder="Username" value={values.username} onChange={handleChange} />
              <label>Username</label> */}
              <Input
                name="username"
                label="username"
                type="text"
                id="username"
                placeholder="Username"
                validation={{
                    required: {
                        value: true,
                        message: 'username field is required' 
                    },
                    pattern: {
                      value: /^[a-zA-Z0-9_]+$/,
                      message: 'username must contain only letters, numbers and underscores'
                    }
                }}
              />
            </p>

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
              {/* <input type="password" name="password" required="" placeholder="Password" value={values.password} onChange={handleChange} />
              <label>password</label> */}
              <Input
                name="password"
                label="password"
                type="password"
                id="password"
                placeholder="Password"
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
            {/* <p>
              <input type="password" name="confirm_password" required="" placeholder="Confirm Password" value={values.confirm_password} onChange={handleChange} />
              <label>Confirm Password</label>
              <span class="show-password-input"></span>
            </p> */}

            <ReCAPTCHA
            sitekey="6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI"
            onChange={onChange}
                />
            <p>
              { loading ? <LoadingButton /> : <input disabled={!verified && loading} value="Sign Up" type="submit" onClick={handleSubmit}/> }
            </p>
            <p class="align-center">
              By continuing you indicate that you read and agreed to the Terms of Use
            </p>
            <p class="d-flex justify-center">
              have an account?<a href="profile-setup">Sign in!</a>
            </p>
          </form>
        </FormProvider>
      </div>
    </div>
              
  );
}

export default RegistrationForm;

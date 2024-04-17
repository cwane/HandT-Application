import React, {useState} from 'react'
import './LoginForm.css'
import { FormProvider, useForm } from 'react-hook-form'
import { useAuthState } from '../../context/context'
import ReCAPTCHA from "react-google-recaptcha";

import { Input } from '../Input/Input'
import LoginLink1 from '../../assets/images/login-link1.png'
import LoginLink2 from '../../assets/images/login-link2.png'
import LoadingButton from '../LoadingButton/LoadingButton'

const LoginForm = ({ methods, handleSubmit }) => {
    const [verified, setVerified] = useState(false)
    // Recaptcha function
    function onChange(value) {
        console.log("Captcha value:", value);
        setVerified(true);
      }
    const { loading, errorMessage } = useAuthState()

  return (
    <div class="login-form-content">
        <div class="login-link-wrap">
            <ul>
                <li>
                    <a href="#">
                        <img src={LoginLink2} alt="" />
                        Google
                    </a>
                </li>
                <li>
                    <a href="#">
                        <img src={LoginLink1} alt="" />
                        facebook
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
                        {/* <input type="text" name="username" required="" placeholder="Username" value={values.username} onChange={handleChange} />
                        <label>Username</label> */}
                        <Input
                            name="username"
                            label="Username"
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
                                },
                            }}
                        />
                    </p>
                    <p>
                        {/* <input type="password" name="password" required="" placeholder="Password" value={values.password} onChange={handleChange} />
                        <label>password</label> */}
                        <Input
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            placeholder="Password"
                            validation={{
                                required: {
                                    value: true,
                                    message: "password field is required"
                                },
                                // minLength: {
                                //     value: 8,
                                //     message: "password must have at least 8 characters"
                                // },
                                // pattern: {
                                //     value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@$!%*?&])[A-Za-z\d#@$!%*?&]{8,}$/,
                                //     message: "password must contain at least one uppercase, one lowercase, one number and one special character"
                                // }
                            }}
                        />
                        <span class="show-password-input"></span>
                    </p>
                    <p class="d-flex justify-between">
                        <label class="label-for-checkbox">
                            <input class="form-input-checkbox" name="rememberme" type="checkbox" id="rememberme" value="forever" /> <span>Remember me</span>
                        </label>
                        <a href="forgot-password" style={{color: "#D93F21"}}>Forgot Password?</a>
                    </p>

                            <ReCAPTCHA
                        sitekey="6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI"
                        onChange={onChange}
                    />,
                    <p>
                        { loading ? <LoadingButton /> : <input disabled={!verified} onClick={handleSubmit} value="Login" type="submit" /> }
                    </p>
                    <p class="d-flex justify-center">
                        Don’t have an account?  <a href="signup-page">Sign up!</a>
                    </p>
                </form>
            </FormProvider>
        </div>
    </div>
  )
}

export default LoginForm
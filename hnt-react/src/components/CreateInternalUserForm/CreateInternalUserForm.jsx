import React from 'react'
import { FormProvider } from 'react-hook-form'
import { Input } from '../Input/Input'
import LoadingButton from '../LoadingButton/LoadingButton'

const CreateInternalUserForm = ({ loading, handleSubmit, methods }) => {
    
  return (
    <div class="login-form-wrap">
        <h1>Add new Internal user</h1>
        <p>Create a brand new internal user and add them to this site</p>
        <FormProvider {...methods}>
            <form onSubmit={e => e.preventDefault()} noValidate>
                <p>
                    {/* <input type="password" name="password" required="" placeholder="Password" value={values.password} onChange={handleChange} />
                    <label>password</label> */}
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

                <p>
                {loading ? <LoadingButton /> : <input value="Create User" onClick={handleSubmit} type="submit" /> }
                </p>
            </form>
        </FormProvider>
    </div>
  )
}

export default CreateInternalUserForm
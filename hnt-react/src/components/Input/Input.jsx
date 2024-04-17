import { useFormContext } from "react-hook-form"
import { findInputError } from '../../utils/findInputError';
import { isFormInvalid } from '../../utils/isFormInvalid';
import './Input.css'

export const Input = ({ name, label, type, id, className, placeholder, validation }) => {
    const {
        register,
        formState: { errors }
    } = useFormContext();

    const inputError = findInputError(errors, name);
    const isInvalid = isFormInvalid(inputError);

    return (
        <>
            <input
                style={{borderColor: isInvalid ? 'red' : '#6B737A'}}
                name={name}
                id={id}
                type={type}
                className={className}
                placeholder={placeholder}
                // defaultValue={"Annapurna base camp"}
                {...register(name, validation)}
            />
            <label htmlFor={id}>
                {label}
            </label>
            { isInvalid && 
                <InputError
                    message={inputError.error.message}
                    key={inputError.error.message}
                />
            }
        </>
    )
}

const InputError = ({message}) => {
    return (
        <p className="error-message" style={{color:'red', marginTop: '4px', marginLeft: '15px', fontSize: '12px'}}>
            {message}
        </p>
    )
}
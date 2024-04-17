import axios from "axios";

export async function loginUser(dispatch, loginPayload) {
    try {
        dispatch({ type: 'REQUEST_LOGIN' });
        let response = await axios.post(`${process.env.REACT_APP_ROOT_URL}/Authentication/login`, loginPayload)
        
        if (response.data.data.token) {
            dispatch({ type: 'LOGIN_SUCCESS', payload: response.data.data });
            localStorage.setItem('currentUser', JSON.stringify(response.data.data));
            return response.data.data;
        }
    } catch(error) {
        dispatch({ type: 'LOGIN_ERROR', error: error.response.data.message });
    }
}

export async function logout(dispatch) {
    dispatch({ type: 'LOGOUT' });
    localStorage.removeItem('currentUser');
}
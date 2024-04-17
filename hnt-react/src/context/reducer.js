import React, { useReducer } from 'react';

let roles = localStorage.getItem('currentUser') ? JSON.parse(localStorage.getItem('currentUser')).roles : '';
let username = localStorage.getItem('currentUser') ? JSON.parse(localStorage.getItem('currentUser')).username : '';
let token = localStorage.getItem('currentUser') ? JSON.parse(localStorage.getItem('currentUser')).token : '';

export const initialState = {
    roles: '' || roles,
    username: '' || username,
    token: '' || token,
    loading: false,
    errorMessage: null
}

export const authReducer = (initialState, action) => {
    switch(action.type) {
        case "REQUEST_LOGIN":
            return {
                ...initialState,
                loading: true
            }
        case "LOGIN_SUCCESS":
            console.log("roles", action.payload.roles);
            return {
                ...initialState,
                roles: action.payload.roles,
                username: action.payload.username || '',
                token: action.payload.token,
                loading: false
            }
        case "LOGOUT":
            return {
                ...initialState,
                roles: [],
                username: '',
                token: ''
            }
        case "LOGIN_ERROR":
            return {
                ...initialState,
                loading: false,
                errorMessage: action.error
            }
        default:
            throw new Error(`Unhandled action type: ${action.type}`)
    }
}
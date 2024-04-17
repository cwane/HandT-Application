import { Slide, toast } from 'react-toastify';

export const toastSettings = {
    position: toast.POSITION.BOTTOM_CENTER,
    autoClose: 3000, //3 seconds
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    transition: Slide
}
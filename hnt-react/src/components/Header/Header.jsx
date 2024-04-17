import React, { useEffect, useRef, useState } from 'react'
import { useAuthDispatch, useAuthState } from '../../context/context'
import { logout } from '../../context/actions';
import { toast } from 'react-toastify';
import { toastSettings } from '../../utils/toastSettings';
import 'react-toastify/dist/ReactToastify.css';
import { useLocation, useNavigate } from 'react-router-dom'
import { CgProfile } from "react-icons/cg";
import { IoIosArrowDown } from "react-icons/io";
import { FaPencil } from "react-icons/fa6";
import { AiFillDashboard } from "react-icons/ai";
import { IoBookmark } from "react-icons/io5";
import { GiNotebook } from "react-icons/gi";
import { BsGraphUpArrow } from "react-icons/bs";
import { IoSettings } from "react-icons/io5";
import { CiLogout } from "react-icons/ci";


import './Header.css'
import SITE_LOGO from '../../assets/images/site-logo.png'
import SEARCH_ICON from '../../assets/images/search-icon.png'

const Header = () => {
  const location = useLocation().pathname;
  const userDetails = useAuthState();

  return (
    <>
      <header id="masthead" class={`site-header ${location !== '/' ? 'static' : ''}`}>
        <div class="hgroup-wrap">
          <div class="container-fluid">
            <div class="site-branding">
              <h1 class="site-title">
                <a href="/" target="_self">
                  <img src={SITE_LOGO} alt="" />
                </a>
              </h1>
            </div>
            {/* site branding */}

            <div class="hgroup-right">
              <div class="header-search">
                <span class="search-toggle">
                  <img src={SEARCH_ICON} alt="" />
                </span>
                <form role="search" class="search-form">
                    <label>
                        <span class="screen-reader-text">Search for:</span>
                        <input class="search-field" placeholder="Search for an event near you..." value="" name="s" type="search" />
                    </label>
                    <input class="search-submit" value="Search" type="submit" />
                </form>
              </div>
              <div id="navbar" class="navbar">
                <nav id="site-navigation" class="navigation main-navigation">
                  <div class="menu-top-menu-container">
                    <ul>
                      <li>
                        <a href="/create-event" target="_self">Create Event</a>
                      </li>
                      <li>
                        <a href="event-listing" target="_self">Events Nearby</a>
                      </li>
                      <li>
                        <a href="#" target="_self">About</a>
                      </li>
                      <li>
                        <a href="#" target="_self">Help</a>
                      </li>
                    </ul>
                  </div>
                </nav>
              </div>
              <div class="header-signin">
                {
                  userDetails.token ? <ProfileIcon userDetails={userDetails} /> : <a href="login-page">sign in</a>
                }
              </div>

              {/* navbar ends here */}
            </div>
            {/* .hgroup-right */}
          </div>
          {/* .container */}
        </div>
        {/* .hgroup-wrap */}
      </header>
      {/* header ends here */}
    </>
  )
}

export default Header

const ProfileIcon = ({ userDetails }) => {
  const [open, setOpen] = useState(false);
  const dispatch = useAuthDispatch();
  const menuRef = useRef();
  
  const handleLogout = () => {
    try {
      logout(dispatch);
      toast.success('Logout Successful', toastSettings);
    } catch (error) {
        toast.error(error, toastSettings);
    }
  }

  useEffect(() => {
    let handler = (e) => {
      if (!menuRef.current.contains(e.target)) {
        setOpen(false);
      }
    };
    document.addEventListener("mousedown", handler);
    return () => {
      document.removeEventListener("mousedown", handler);
    };
  });

  return (
    <>
      <div className='menu-container' ref={menuRef}>
        <div className='menu-trigger' onClick={() => setOpen(!open)}>
          <CgProfile size={30} color='white'/>
          <IoIosArrowDown size={16} color='white'/>
        </div>

        <div className={`dropdown ${open ? 'active': 'inactive'}`}>
          <div className='user-profile'>
            <img src={"https://picsum.photos/200/300"} alt="" />
            <div className="user-details">
              <h6>{userDetails.username}</h6>
              <p>user@example.com</p>
            </div>
          </div>
          <ul className='dropdown-list'>
            <DropDownItem icon={<FaPencil size={14} color='#777777'/>} text={"Edit Profile"} to="profile-setup"/>
            <DropDownItem icon={<AiFillDashboard size={14} color='#777777'/>} text={"User Dashboard"} to="profile-page"/>
            <DropDownItem icon={<IoBookmark size={14} color='#777777'/>} text={"Saved Events"}/>
            <DropDownItem icon={<GiNotebook size={14} color='#777777'/>} text={"My Drafts"}/>
            <DropDownItem icon={<BsGraphUpArrow size={14} color='#777777'/>} text={"Event Dashboard"}/>
            <DropDownItem icon={<IoSettings size={14} color='#777777'/>} text={"Setting"}/>  
          </ul>
          <div className="logout-container">
            <div className="dropdown-item" onClick={() => handleLogout()}>
              <p><CiLogout size={14} color='#777777'/></p>
              <p>Log Out</p>
            </div>
          </div>
        </div>
      </div>

      
    </>
  )
}

const DropDownItem = (props) => {
  const navigate = useNavigate();
  const handleNavigation = () => {
    navigate(props.to);
  }

  return (
    <div className="dropdown-item" onClick={() => handleNavigation()}>
      <p>{props.icon}</p>
      <p>{props.text}</p>
    </div>
  )
}
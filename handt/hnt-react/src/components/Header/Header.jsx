import React from 'react'
import './Header.css'

import SITE_LOGO from '../../assets/images/site-logo.png'
import SEARCH_ICON from '../../assets/images/search-icon.png'

const Header = () => {
  return (
    <>
      <header id="masthead" class="site-header">
        <div class="hgroup-wrap">
          <div class="container-fluid">
            <div class="site-branding">
              <h1 class="site-title">
                <a href="index.html" target="_self">
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
                        <a href="#" target="_self">Create Event</a>
                      </li>
                      <li>
                        <a href="#" target="_self">Events Nearby</a>
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
                <a href="login-page.html">sign in</a>
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
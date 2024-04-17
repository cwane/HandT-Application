import React from 'react';
import Profile from '../../components/Profile/Profile';
import ProfilePageTitleImage from '../../assets/images/profile-page-title-image.png'
import './ProfilePage.css'

const Profilepage = () => {
  return (
    <>
    <section class="page-title-wrap" style={{backgroundImage: "url(" + ProfilePageTitleImage + ")"}}>
        <div class="container">
          <h2 class="page-title screen-reader-text">
            All Themes Plan Package
          </h2>
        </div>
      </section>
    <Profile/>
   </>
  );
}

export default Profilepage;

import React, { useEffect, useState } from 'react';
import './Profile.css';
import axios from 'axios';
import AuthorProfileImage from '../../assets/images/author-profile-image.png'
import ReviewAuthorImage from '../../assets/images/review-author-image.png'
import { useAuthState } from '../../context/context'


const Profile = () => {
  const authentication = useAuthState()
  const [profileData, setProfileData] = useState(null);

  const [loading, setLoading] = useState(false);

  useEffect(() => {
    
    const fetchProfileData = async () => {
      try {
        setLoading(true)
        const response = await axios.get(
          'https://localhost:7037/api/Authentication/get-user-detail', 
          {
            headers: {
              Authorization: `Bearer ${authentication.token}`,
            },
          }
        );

        if (response.status === 200) {
          // if (typeof response.data.interest === 'string') {
            
          //   response.data.interest = response.data.interest.split(',');
          // }
         
          setProfileData(response.data.data);
          console.log(response.data.data)
          console.log(profileData)
        } else {
          console.error('Failed to fetch profile data:', response.statusText);
        }
        setLoading(false)
      } catch (error) {
        console.error('An error occurred while fetching profile data:', error.response.data);
      }
      
    };
    
    fetchProfileData(); 
  }, []); 

  if (loading) {
    return <p>Loading...</p>;
  }

  if (!profileData) {
    return <p>Error loading profile data</p>;
  }


  return (
    
    <div id="content" class="site-content">
      <div id="primary" class="content-area">
        <main id="main" class="site-main">

        {loading? <p>Loading</p> : 
        
          
          <section class="profile-page-wrap" >

            <div class="container">
              <div class="row">
                <div class="col-sm-4">
                  <div class="profile-sidebar-wrap">
                    <div class="author-profile-edit-wrap">
                      <figure class="featured-image">
                        <img src={`https://localhost:7037/Upload/${profileData.picture}?${new Date().getTime()}`} alt="" />
                       
                      </figure>
                      <h3 class="author-name">{loading ? 'Loading...' : profileData.fullname}</h3>
                      <a href="profile-setup" class="box-button">Edit profile</a>
                    </div>
                    <div class="author-individual-info-wrap">
                      <ul>
                        <li>
                          <a href="#"><i class="fa fa-map-marker"></i>
                            {loading ? 'Loading...' : profileData.address}
                          </a>
                        </li>
                        <li>
                          <a href="#"><i class="fa fa-phone"></i>
                            {loading ? 'Loading...' : profileData.contact_no}
                          </a>
                        </li>
                        <li>
                          <a href="#"><i class="fa fa-user"></i>
                           {loading ? 'Loading...' : profileData.gender}
                          </a>
                        </li>
                        <li>
                          <a href="#"><i class="fa fa-briefcase"></i>
                            {loading ? 'Loading...' : profileData.occupation}
                          </a>
                        </li>
                      </ul>
                    </div>
                    {/* <div class="author-interest-wrap">
                      <h3 class="widget-title">Interests</h3>
                      <ul>
                        {profileData.interest.map((interest, index) => (
                          <li key={index}>
                            <a href="#">{interest.trim()}</a>
                          </li>
                        ))}
                      </ul>
                      <div class="see-all-wrap">
                        <a href="#">see all</a>
                      </div>
                    </div> */}
                    <div class="author-personal-info-wrap">
                      <h3 class="widget-title">About Me</h3>
                      <p>{loading ? 'Loading...' : profileData.bio} </p>
                        <div class="see-all-wrap">
                          <a href="#">see more</a>
                        </div>
                    </div>
                  </div>
                </div>
                <div class="col-sm-8">
                  <div class="profile-info-wrap">
                    <div class="tab-wrapper">
                      <nav>
                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                        <button class="nav-link " data-bs-toggle="tab" data-bs-target="#author-profile-tab"><i class="fa fa-user"></i> Events Created</button>
                          <button class="nav-link " data-bs-toggle="tab" data-bs-target="#author-profile-tab"><i class="fa fa-user"></i> Profile</button>
                          <button class="nav-link" data-bs-toggle="tab" data-bs-target="#author-picture-tab"><i class="fa fa-users"></i> Pictures</button>
                          <button class="nav-link" data-bs-toggle="tab" data-bs-target="#author-connection-tab"><i class="fa fa-users"></i> connections</button>
                        </div>
                      </nav>
                      <div class="tab-content" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="author-profile-tab" role="tabpanel">
                          <div id="counter" class="counter-section">
                            <div class="counter-item-wrapper">
                              <div class="counter-item">
                                <div class="count-text-wrapper">
                                  <span class="counter-value" data-count="367">0</span>
                                </div>
                                <span class="counter-name">Events Created</span>
                              </div>
                              <div class="counter-item">
                                <div class="count-text-wrapper">
                                  <span class="counter-value" data-count="15">0</span>
                                </div>
                                <span class="counter-name">Events Participated</span>
                              </div>
                              <div class="counter-item">
                                <div class="count-text-wrapper">
                                  <span class="counter-value" data-count="150">0</span>
                                </div>
                                <span class="counter-name">followers</span>
                              </div>
                              <div class="counter-item">
                                <div class="count-text-wrapper">
                                  <span class="counter-value" data-count="234">0</span>
                                </div>
                                <span class="counter-name">Following</span>
                              </div>
                            </div>
                          </div>
                          <div class="profile-review-section">
                            <div class="overall-rating-wrap">
                              <div class="rating-title">
                                <h3 class="entry-title">Overall Rating:</h3>
                              </div>
                              <div class="star-ratings">
                                <div class="star-ratings-top" style={{width: '90%'}}>
                                  <span>★</span>
                                  <span>★</span>
                                  <span>★</span>
                                  <span>★</span>
                                  <span>★</span>
                                </div>
                                <div class="star-ratings-bottom">
                                  <span>★</span>
                                  <span>★</span>
                                  <span>★</span>
                                  <span>★</span>
                                  <span>★</span>
                                </div>
                              </div>
                            </div>
                            <div class="profile-review-title-wrap">
                              <h3 class="entry-title">All Reviews (<span>5</span>) </h3>
                              <a href="#"><i class="fa fa-plus"></i> ADD REVIEW</a>
                            </div>
                            <div class="profile-review-item-wrap">
                              <div class="profile-review-item">
                                <div class="profile-review-content">
                                  <div class="author-info-wrap">
                                    <figure class="featured-image">
                                      <img src={ReviewAuthorImage} alt="" />
                                    </figure>
                                    <div class="author-info">
                                      <h6 class="author-name">sanjay S.</h6>
                                      <div class="star-ratings">
                                        <div class="star-ratings-top" style={{width: '100%'}}>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                        </div>
                                        <div class="star-ratings-bottom">
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                        </div>
                                      </div>
                                    </div>
                                  </div>
                                  <div class="entry-content">
                                    <p>Amet minim mollit non deserunt ullamco est sit aliqua dolor do amet sint. Velit 
                                      officia consequat duis enim velit mollit. Exercitation veniam consequat sunt 
                                      nostrud amet. Amet minim mollit non deserunt ullamco est sit aliqua dolor do amet 
                                      sint.
                                    </p>
                                    <p>Amet minim mollit non deserunt ullamco est sit aliqua dolor do amet sint.</p>
                                  </div>
                                </div>
                              </div>
                              <div class="profile-review-item">
                                <div class="profile-review-content">
                                  <div class="author-info-wrap">
                                    <figure class="featured-image">
                                      <img src={ReviewAuthorImage} alt="" />
                                    </figure>
                                    <div class="author-info">
                                      <h6 class="author-name">sanjay S.</h6>
                                      <div class="star-ratings">
                                        <div class="star-ratings-top" style={{width: '100%'}}>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                        </div>
                                        <div class="star-ratings-bottom">
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                          <span>★</span>
                                        </div>
                                      </div>
                                    </div>
                                  </div>
                                  <div class="entry-content">
                                    <p>Amet minim mollit non deserunt ullamco est sit aliqua dolor do amet sint. Velit 
                                      officia consequat duis enim velit mollit. Exercitation veniam consequat sunt 
                                      nostrud amet. Amet minim mollit non deserunt ullamco est sit aliqua dolor do amet 
                                      sint.
                                    </p>
                                    <p>Amet minim mollit non deserunt ullamco est sit aliqua dolor do amet sint.</p>
                                  </div>
                                </div>
                              </div>
                              <a href="#" class="box-button">load more</a>
                            </div>
                          </div>
                        </div>
                        <div class="tab-pane fade" id="author-picture-tab" role="tabpanel">
                          Picture....
                        </div>
                        <div class="tab-pane fade" id="author-connection-tab" role="tabpanel">
                          Connection ...
                        </div>
                      </div>
      
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </section> }
         
        </main>
        
      </div>
     
    </div>
  );
}

export default Profile;

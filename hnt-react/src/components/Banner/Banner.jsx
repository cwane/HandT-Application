import React from 'react'

import './Banner.css'

import BANNER_IMAGE from '../../assets/images/banner-image.png'

const Banner = () => {
  return (
    <section class="banner-section" style={{backgroundImage: "url(" + BANNER_IMAGE + ")"}}>
            <div class="container">
              <div class="banner-content">
                <div class="banner-text">
                    <h2 class="entry-title">Make Your Life Worthwhile</h2>
                    <p>by creating or registering for a trekking, hiking, & adventure
                      events!</p>
                    <a href="#" class="box-button">create event</a>
                </div>
              </div>
            </div>
            <div class="banner-bg-color"></div>
    </section>
  )
}

export default Banner
import React from 'react'

import Slider from 'react-slick/lib/slider'
import "slick-carousel/slick/slick.css"; 
import "slick-carousel/slick/slick-theme.css";

import './FeaturedEvents.css'
import PackageItemCard from '../PackageItemCard/PackageItemCard'

const FeaturedEvents = () => {
    const settings = {
      dots: true,
      infinite: true,
      speed: 600,
      autoplay: false,
      fade: false,
      arrows: true,
      slidesToShow: 3,
      responsive: [{
          breakpoint: 991,
          settings: {
            slidesToShow: 2
          },
          breakpoint: 576,
          settings: {
            slidesToShow: 1
          }
        }
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
      ]
    }

  return (
    <section class="featured-event-section">
      <div class="container">
        <header class="entry-header heading">
          <h2 class="entry-title">
            Featured Events
          </h2>
        </header>
        
        <Slider {...settings} className="featured-event-slider">
          <PackageItemCard />
          <PackageItemCard />
          <PackageItemCard />
          <PackageItemCard />
        </Slider>
      </div>
    </section>
  )
}

export default FeaturedEvents
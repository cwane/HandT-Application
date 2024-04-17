import React, { useState, useEffect } from 'react'

import Slider from 'react-slick/lib/slider'
import "slick-carousel/slick/slick.css"; 
import "slick-carousel/slick/slick-theme.css";

import './FeaturedEvents.css'
import PackageItemCard from '../PackageItemCard/PackageItemCard'

import axios from 'axios';

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

    const [featuredEvents, setFeaturedEvents] = useState([]);

    useEffect(() => {
      // Fetch featured events from the API when the component mounts
      axios.get(`${process.env.REACT_APP_ROOT_URL}/Front/Featured-Events`)
        .then(response => {
          setFeaturedEvents(response.data);
        })
        .catch(error => {
          console.error(error);
        });
    }, []);

  return (
    <section class="featured-event-section">
      <div class="container">
        <header class="entry-header heading">
          <h2 class="entry-title">
            Featured Events
          </h2>
        </header>
        
        <Slider {...settings} className="featured-event-slider">

        {featuredEvents.map((event) => (
          <PackageItemCard key={event.id} event={event} />
        ))}


          {/* <PackageItemCard />
          <PackageItemCard />
          <PackageItemCard />
          <PackageItemCard /> */}
        </Slider>
      </div>
    </section>
  )
}

export default FeaturedEvents
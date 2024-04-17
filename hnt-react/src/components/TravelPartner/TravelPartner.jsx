import React from 'react'

import Slider from 'react-slick/lib/slider'
import "slick-carousel/slick/slick.css"; 
import "slick-carousel/slick/slick-theme.css";

import './TravelPartner.css'

import Partner1 from '../../assets/images/partner1.png'
import Partner2 from '../../assets/images/partner2.png'

const TravelPartner = () => {
    const settings = {
        dots: false,
        infinite: true,
        speed: 600,
        autoplay: false,
        fade: false,
        arrows: false,
        slidesToShow: 6,
        responsive: [{
            breakpoint: 991,
            settings: {
            slidesToShow: 4
            },
            breakpoint: 576,
            settings: {
            slidesToShow: 3
            },
            breakpoint: 479,
            settings: {
            slidesToShow: 2
            }
        }
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
        ]
    }
    
  return (
    <section class="travel-partner-section">
        <div class="container">
            <Slider {...settings} class="travel-partner-slider">
                <figure class="travel-partner-item">
                    <a href="#">
                        <img src={Partner1} alt="" />
                    </a>
                </figure>
                <figure class="travel-partner-item">
                    <a href="#">
                        <img src={Partner2} alt="" />
                    </a>
                </figure>
                <figure class="travel-partner-item">
                    <a href="#">
                        <img src={Partner1} alt="" />
                    </a>
                </figure>
                <figure class="travel-partner-item">
                    <a href="#">
                        <img src={Partner2} alt="" />
                    </a>
                </figure>
                <figure class="travel-partner-item">
                    <a href="#">
                        <img src={Partner1} alt="" />
                    </a>
                </figure>
                <figure class="travel-partner-item">
                    <a href="#">
                        <img src={Partner2} alt="" />
                    </a>
                </figure>
                <figure class="travel-partner-item">
                    <a href="#">
                        <img src={Partner1} alt="" />
                    </a>
                </figure>
                <figure class="travel-partner-item">
                    <a href="#">
                        <img src={Partner2} alt="" />
                    </a>
                </figure>
            </Slider>
        </div>
    </section>
  )
}

export default TravelPartner
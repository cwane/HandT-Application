import React from 'react'
import './SpecialOffer.css'

import OfferImage from '../../assets/images/offer-image.png'
import DotBackground from '../../assets/images/dot-bg.png'


const SpecialOffer = () => {
  return (
    <section class="special-offer-section">
        <div class="container">
            <div class="special-offer-content-wrapper">
                <div class="special-offer-image-wrap">
                    <figure class="featured-image">
                        <img src={OfferImage} alt="" />
                    </figure>
                </div>
                <div class="special-form-wrap">
                    <div class="special-form-content-wrap">
                        <header class="entry-header heading">
                            <h3 class="entry-subtitle">
                            Subscribe For Offers
                            </h3>
                            <h2 class="entry-title">
                            Get special offers, and more from H&T
                            </h2>
                            <p>Be the first to know about exclusive offers and enjoy reduced prices by subscribing today!</p>
                        </header>
                    </div>
                    <form class="mc4wp-form">
                        <div class="mc4wp-form-fields">
                            <p>
                                <input name="email" placeholder="Email: " required="" type="email" />
                            </p>
                            <p class="mc4wp-submit-btn">
                                <input value="SUBSCRIBE" type="submit" />
                            </p>
                            <label style={{display: "none"}}>Leave this field empty if you're human:
                                <input name="_mc4wp_honeypot" value="" tabindex="-1" autocomplete="off" type="text" />
                            </label>
                            <input name="_mc4wp_timestamp" value="1507111963" type="hidden" />
                            <input name="_mc4wp_form_id" value="1732" type="hidden" />
                            <input name="_mc4wp_form_element_id" value="mc4wp-form-1" type="hidden" />
                        </div>
                        <div class="mc4wp-response"></div>
                    </form>
                </div>
            </div>
            <span class="dot-bg-image">
                <img src={DotBackground} alt="" />
            </span>
        </div>
    </section>
  )
}

export default SpecialOffer
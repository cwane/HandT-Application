import React from 'react'

import './Footer.css'
import '../../index.css'

import FooterBG from '../../assets/images/footer-bg.png'
import SiteLogo from '../../assets/images/site-logo.png'
import PaymentIcon from '../../assets/images/payment-icon1.svg'

const Footer = () => {
  return (
    <footer id="colophon" class="site-footer">
      
        <section class="widget-area" style={{backgroundImage: "url(" + FooterBG + ")"}}>
            <div class="container">
                <div class="row">
                    <div class="col-sm-3">
                        <aside class="widget">
                            <div class="textwidget">
                                <a href="#">
                                    <img src={SiteLogo} alt="" />
                                </a>
                            </div>
                        </aside>
                    </div>
                    <div class="col-sm-3">
                        <aside class="widget">
                            <h2 class="widget-title">Quick Links</h2>
                            <ul>
                                <li>
                                    <a href="#" title="Create Events">Create Events</a>
                                </li>
                                <li>
                                    <a href="#" title="Nearby Events">Nearby Events</a>
                                </li>
                                <li>
                                    <a href="#" title="Help">Help</a>
                                </li>
                            </ul>
                        </aside>
                    </div>
                    <div class="col-sm-3">
                        <aside class="widget">
                            <h2 class="widget-title">Company</h2>
                            <ul>
                                <li>
                                    <a href="#" title="About Us">About Us</a>
                                </li>
                                <li>
                                    <a href="#" title="Careers">Careers</a>
                                </li>
                                <li>
                                    <a href="#" title="FAQs">FAQs</a>
                                </li>
                                <li>
                                    <a href="#" title="Teams">Teams</a>
                                </li>
                                <li>
                                    <a href="#" title="Contact Us">Contact Us</a>
                                </li>
                            </ul>
                        </aside>
                    </div>
                    <div class="col-sm-3">
                        <aside class="widget">
                            <h2 class="widget-title">Follow Us</h2>
                            <div class="inline-social-icon">
                                <ul>
                                    <li>
                                        <a href="facebook.com">facebook</a>
                                    </li>
                                    <li>
                                        <a href="twitter.com">twitter</a>
                                    </li>
                                    <li>
                                        <a href="instagram.com">instagram</a>
                                    </li>
                                    <li>
                                        <a href="linkedin.com">linkedin</a>
                                    </li>
                                </ul>
                            </div>
                        </aside>
                        <aside class="widget">
                            <h2 class="widget-title">Subscribe</h2>
                            <div class="mc4wp-form-wrapper">
                            <p>Subscribe to stay tuned for new web design and latest updates. Let's do it! </p>
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
                        </aside>
                    </div>
                </div>
            </div>
          {/* .container */}
        </section>

        <div class="site-generator">
          <div class="container">
            <div class="row">
              <div class="col-sm-4">
                <div class="footer-menu">
                  <ul>
                    <li><a href="#"><img src={PaymentIcon} alt="" /></a></li>
                    <li><a href="#"><img src={PaymentIcon} alt="" /></a></li>
                    <li><a href="#"><img src={PaymentIcon} alt="" /></a></li>
                    <li><a href="#"><img src={PaymentIcon} alt="" /></a></li>
                    <li><a href="#"><img src={PaymentIcon} alt="" /></a></li>
                    <li><a href="#"><img src={PaymentIcon} alt="" /></a></li>
                  </ul>
                </div>
              </div>
              <div class="col-sm-5">
                <div class="footer-menu">
                  <ul>
                    <li><a href="#">Privacy Policy</a></li>
                    <li>
                      <a href="#">Terms of Use</a>
                    </li>
                    <li>
                      <a href="#">Refunds</a>
                    </li>
                    <li>
                      <a href="#">Site Map</a>
                    </li>
                  </ul>
                </div>
              </div>
              <div class="col-sm-3">
                <span class="copy-right">
                  © 2021 All Rights Reserved
                </span>
              </div>
            </div>
          </div>
        </div>
        {/* .container */}
      
      {/* .site-generator */}
    {/* .site-footer */}
    </footer>
  )
}

export default Footer
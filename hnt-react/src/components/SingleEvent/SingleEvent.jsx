import React,{useState, useEffect} from 'react';
import EventGallery1 from '../../assets/images/event-gallery1.png';
import EventGallery2 from '../../assets/images/event-gallery2.png';
import EventGallery3 from '../../assets/images/event-gallery3.png';
import EventGallery4 from '../../assets/images/event-gallery4.png';
import EventGallery5 from '../../assets/images/event-gallery5.png';
import PackageAuthorImage from '../../assets/images/package-author-image.png';
import DestinationIcon from '../../assets/images/destination-icon.png';
import { useParams } from 'react-router-dom';
import { useAuthState } from '../../context/context'

import Slider from 'react-slick/lib/slider'
import "slick-carousel/slick/slick.css"; 
import "slick-carousel/slick/slick-theme.css";

import axios from 'axios';

const SingleEvent = () => {
const { eventId } = useParams();
const [expanded, setexpanded] = useState(false);
const [expandedAccordion, setExpandedAccordion] = useState(null);
const authentication = useAuthState()

const sliderSettings = {
  dots: true,
  infinite: true,
  speed: 500,
  slidesToShow: 1,
  slidesToScroll: 1,
};


const toggleDescription = () => {
  setexpanded(!expanded);
  console.log("Toggle description called");
  console.log("Expanded:", !expanded);
};

const handleAccordionToggle = (accordionId) => {
  setExpandedAccordion((prev) => (prev === accordionId ? null : accordionId));
};

const [viewEvents, setViewEvents] = useState([]);




useEffect(() => {
  axios.get(`${process.env.REACT_APP_ROOT_URL}/Events/View-Events?eventid=${eventId}`, {
    headers: {
      
      Authorization: `Bearer ${authentication.token}`,
    },
  })
  .then(response => {
    console.log('API Response:', response);
    setViewEvents(response.data);
  })
  .catch(error => {
    console.error(error);
  });
}, [eventId]);


console.log('View Events:', viewEvents);


  return (

        
    <div id="content" class="site-content">
      <div id="primary" class="content-area">
        <main id="main" class="site-main" >
        {viewEvents.map(event => (
          
            <section class="single-page-wrap" key={event.id}>

          
            <div class="container">
              <div class="single-page-content-wrap">
                <div class="single-page-leftbar">
                  <article class="post">
                    <div className="entry-header">
                      <h3 class="entry-title">{event.event_title}</h3>
                      <div class="event-detail-info-detail">
                        <div class="event-detail-duration-info">
                          <i class="fa fa-clock-o"></i>
                          <h5>{event.days} days</h5> 
                          <a href="#">(July 23 - July 28, 2023)</a> 
                        </div>
                        <div class="event-detail-location-info">
                          <ul>
                            <li><a href="#">Pokhara</a></li>
                            <li><a href="#">Annapurna</a></li>
                            <li><a href="#">Nepal</a></li>
                          </ul>
                        </div>
                      </div>
                      <div class="post-image-wrapper">
                      <Slider {...sliderSettings} class="post-big-image-slider">
                            <div class="featured-image">
                              <img src={EventGallery1} alt="" />
                            </div>
                            <div class="featured-image">
                              <img src={EventGallery2} alt="" />
                            </div>
                            <div class="featured-image">
                              <img src={EventGallery3} alt="" />
                            </div>
                            <div class="featured-image">
                              <img src={EventGallery4} alt="" />
                            </div>
                            <div class="featured-image">
                              <img src={EventGallery5} alt="" />
                            </div>
                      </Slider>
                        </div>
                        <Slider {...sliderSettings} class="slider-nav-thumbnails">
                        
                          <div class="nav-thumbnails-wrap">
                            <img src={EventGallery1} alt="" />
                          </div>
                          <div class="nav-thumbnails-wrap">
                            <img src={EventGallery2} alt="" />
                          </div>
                          <div class="nav-thumbnails-wrap">
                            <img src={EventGallery3} alt="" />
                          </div>
                          <div class="nav-thumbnails-wrap">
                            <img src={EventGallery4} alt="" />
                          </div>
                          <div class="nav-thumbnails-wrap">
                            <img src={EventGallery5} alt="" />
                          </div>
                          </Slider>
                        
                      
                    </div>
                    <div class="post-discription-wrap" >
                      <h3>Description</h3>
                    <div className={`description-content ${expanded ? 'expanded' : ''}`}>
                      <p>All travelers dream to visit special places during their journey and Everest Base Camp Trek is 
                        the best option one can choose for trekking. Mount Everest, also known as the third pole of the 
                        world will be in front of your eyes on this trip. Continuous effort and desire to be on the base 
                        camp will make the trek successful.</p>

                      <div class="discription-itenery-content">
                        <p>Day 1 : Arrival at Kathmandu and transfer to Hotel (1,350m).</p>
                        <p>Day 2 : Fly to Lukla (2,804m) & trek to Phakding (2,640m) Duration: 4-5 hours.
                        </p>
                        <p>Day 3 : Phakding to Namche Bazaar (3440m/11283) Walking Distance - 10 to 12 km, Duration: 6 hours
                        </p>
                        <p>Day 4 : Namche to Khumjung and Explore Kumjung and kunde Village (3,962m/12995ft) Walking Distance - 3 to 4 km, Duration: 2 to 3 hours
                        </p>
                        <p>Day 5 : Khumjung to Phorste (3860m/12660ft) Walking Distance - 10 to 11 km, Duration: 5 hours
                        </p>
                        <p>Day 6 : Phorste to Dingboche (4410m/14464ft) Walking Distance - 11 to 12 km, Duration: 5 hours</p>
                        <p>Day 7 : Arrival at Kathmandu and transfer to Hotel (1,350m).</p>
                        <p>Day 8 : Fly to Lukla (2,804m) & trek to Phakding (2,640m) Duration: 4-5 hours.
                        </p>
                        <p>Day 9 : Phakding to Namche Bazaar (3440m/11283) Walking Distance - 10 to 12 km, Duration: 6 hours
                        </p>
                        <p>Day 10 : Namche to Khumjung and Explore Kumjung and kunde Village (3,962m/12995ft) Walking Distance - 3 to 4 km, Duration: 2 to 3 hours
                        </p>
                        <p>Day 11 : Khumjung to Phorste (3860m/12660ft) Walking Distance - 10 to 11 km, Duration: 5 hours
                        </p>
                      </div>

                      </div>
                      {/* <a href="javascript:void(0);" class="show-more" onClick={toggleDescription}>show more</a> */}
                      <a href="javascript:void(0);" class="show-more" onClick={toggleDescription}>
                        {expanded ? 'Show Less' : 'Show More'}
                      </a>
                        
                                            
                    </div>
                    <div class="accordion-wrapper">
                      <div class="accordion" id="accordionExample">
                        <div class="accordion-item">
                          <h4 class="accordion-header" id="headingOne">
                            <button class="accordion-button" type="button" onClick={() => handleAccordionToggle('collapseOne')} data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                              Day 1: Kathmandu to Lukla (flight) - Phakding
                            </button>
                          </h4>
                          <div id="collapseOne" class={`accordion-collapse collapse ${expandedAccordion === 'collapseOne' || !expandedAccordion ? 'show' : ''}`} aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                              <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Eligendi voluptas delectus totam consectetur, 
                                vitae reprehenderit explicabo mollitia esse vel deleniti asperiores nesciunt, similique commodi aut 
                                iusto aliquam, dicta recusandae quos!</p>
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Eligendi voluptas delectus totam consectetur, 
                                  vitae reprehenderit explicabo mollitia esse vel deleniti asperiores nesciunt</p>
                            </div>
                          </div>
                        </div>
                        <div class="accordion-item">
                          <h4 class="accordion-header" id="headingTwo">
                            <button class="accordion-button collapsed" type="button" onClick={() => handleAccordionToggle('collapseTwo')} data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo" >
                              Day 2: Phakding - Namche Bazaar
                            </button>
                          </h4>
                          <div id="collapseTwo" class={`accordion-collapse collapse ${expandedAccordion === 'collapseTwo' ? 'show' : ''}`} aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                              <p>Dolor sit amet consectetur adipisicing elit. Eligendi voluptas Lorem ipsum dolor sit amet consectetur adipisicing elit. Eligendi voluptas delectus totam consectetur, 
                                vitae reprehenderit explicabo mollitia esse vel deleniti asperiores nesciunt, similique commodi aut 
                                iusto aliquam, dicta recusandae quos!</p>
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Eligendi voluptas delectus totam consectetur, 
                                  vitae reprehenderit explicabo mollitia esse vel deleniti asperiores nesciunt</p>
                            </div>
                          </div>
                        </div>
                        <div class="accordion-item">
                          <h4 class="accordion-header" id="headingThree">
                            <button class="accordion-button collapsed" type="button" onClick={() => handleAccordionToggle('collapseThree')} data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                              Day 3: Namche Bazar (acclimatization)
                            </button>
                          </h4>
                          <div id="collapseThree" class={`accordion-collapse collapse ${expandedAccordion === 'collapseThree' ? 'show' : ''}`} aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                              <p>Eligendi voluptas delectus totam consectetur, vitae reprehenderit explicabo mollitia esse vel deleniti asperiores nesciunt Lorem ipsum dolor sit amet consectetur adipisicing elit. Eligendi voluptas delectus totam consectetur, 
                                vitae reprehenderit explicabo mollitia esse vel deleniti asperiores nesciunt, similique commodi aut 
                                iusto aliquam, dicta recusandae quos!</p>
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Eligendi voluptas delectus totam consectetur, 
                                  vitae reprehenderit explicabo mollitia esse vel deleniti asperiores nesciunt</p>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="see-all-wrap">
                        <a href="#">see more</a>
                      </div>
                    </div>
                  </article>
                </div>
                <div class="single-page-rightbar">
                  <div class="sidebar-package-info">
                    <div class="sidebar-package-price-info">
                      <div class="sidebar-package-price-detail">
                        <p>From: <strong>Rs.{event.cost_per_person}/</strong> person</p>
                      </div>
                      <div class="sidebar-package-available-detail">
                        <p><strong>5</strong> (Available)</p>
                      </div>
                    </div>
                    <div class="sidebar-package-main-item-wrap">
                      <div class="sidebar-package-main-item">
                        <div class="sidebar-package-main-heading-wrap">
                          <span class="icon-wrapper">
                            <img src="assets/images/calender-black.png" alt="" />
                          </span>
                          <div class="sidebar-package-title-info-wrap">
                            <h5>Pick-up</h5>
                            <span>07/12/2023, 07:30 AM</span>
                          </div>
                        </div>
                      </div>
                      <div class="sidebar-package-main-item">
                        <div class="sidebar-package-main-heading-wrap">
                          <span class="icon-wrapper">
                            <img src="assets/images/calender-black.png" alt="" />
                          </span>
                          <div class="sidebar-package-title-info-wrap">
                            <h5>Drop-off</h5>
                            <span>07/12/2023</span>
                          </div>
                        </div>
                      </div>
                      <div class="sidebar-package-main-item">
                        <div class="sidebar-package-main-heading-wrap">
                          <span class="icon-wrapper">
                            <img src="assets/images/destination-icon.png" alt="" />
                          </span>
                          <div class="sidebar-package-title-info-wrap">
                            <h5>Destination</h5>
                            <span>{event.destination_location}</span>
                          </div>
                        </div>
                        <div class="sidebar-package-map-wrap">
                          <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d14010.152599213669!2d83.86331134123796!3d28.61362909796289!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x39be1ec6808cffc1%3A0x48172cc9dd372cef!2sAnnapurna%20I!5e0!3m2!1sen!2snp!4v1695811289438!5m2!1sen!2snp" height="243"></iframe>
                        </div>
                      </div>
                      <div class="sidebar-package-main-item">
                        <div class="sidebar-package-main-heading-wrap">
                          <span class="icon-wrapper">
                            <img src={DestinationIcon} alt="" />
                          </span>
                          <div class="sidebar-package-title-info-wrap">
                            <h5>Pickup Location</h5>
                            <span>{event.pickup_location}</span>
                          </div>
                        </div>
                        <div class="sidebar-package-map-wrap">
                          <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d14010.152599213669!2d83.86331134123796!3d28.61362909796289!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x39be1ec6808cffc1%3A0x48172cc9dd372cef!2sAnnapurna%20I!5e0!3m2!1sen!2snp!4v1695811289438!5m2!1sen!2snp" height="243"></iframe>
                        </div>
                      </div>
                      <a href="#" class="box-button">book now</a>
                    </div>
                  </div>
                  <div class="sidebar-author-section">
                    <div class="sidebar-package-author-wrap">
                      <figure class="featured-image">
                        <img src={PackageAuthorImage} alt="" />
                      </figure>
                      <div class="package-author-info">
                        <h3 class="author-name">
                          <a href="#">Lyra Innovations</a>
                        </h3>
                        <p>Member Since 2023</p>
                        <span class="package-author-event-count">3 Events</span>
                        <div class="star-ratings">
                          <div class="star-ratings-top" style={{width:'90%'}}>
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
                  </div>
                </div>
              </div>
            </div>
          </section>

        ))}
        </main>
        
      </div>
      
    </div>

   
  );
}

export default SingleEvent;

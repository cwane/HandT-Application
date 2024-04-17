import React, {useState} from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faMapMarker } from '@fortawesome/free-solid-svg-icons'
import { faClock } from '@fortawesome/free-regular-svg-icons'
import { faCalendarMinus } from '@fortawesome/free-regular-svg-icons'
import './PackageItemCard.css'

import ReportIcon from '../../assets/images/report-icon.png'
import EventGallery from '../../assets/images/event-gallery1.png'

const PackageItemCard = ({ event}) => {

    const [isBookmarked, setBookmarked] = useState(false);

    const handleBookmarkClick = () => {
     
      setBookmarked(!isBookmarked);
    };
  return (
    <div class="package-event-item">
        <div class="package-image-wrap">
            <figure class="featured-image">
                <img src={EventGallery} alt="" />
            </figure>
            <span class="event-tag">{event.event_tag}</span>
            </div>
            <div class="package-item-detail">
            <h3 class="entry-title">
                <a href="#">{event.event_title}</a>
            </h3>
            <div class="entry-content">
                <p>{event.event_desc}</p>
            </div>
            <div class="package-item-info-detail">
                <ul>
                <li>
                    <span class="package-info-detail-icon">
                        <FontAwesomeIcon icon={faMapMarker} />
                    </span>
                    {event.destination_location}, {event.location}
                </li>
                <li>
                    <span class="package-info-detail-icon">
                        <FontAwesomeIcon icon={faClock} />
                    </span>
                    {event.days} days
                </li>
                <li>
                    <span class="package-info-detail-icon">
                        <FontAwesomeIcon icon={faCalendarMinus} />
                    </span>
                    {event.starting_date}
                </li>
                </ul>
            </div>
            <div class="package-price-wrap">
                <span class="package-price-rate">Rs. {event.cost_per_person}</span>
                <span class="package-price-person">person</span>
            </div>
            <div class="package-report-wrap">
            <button onClick={handleBookmarkClick}>
            <img src={ReportIcon} alt="" />
          </button>

            </div>
        </div>
    </div>
  )
}

export default PackageItemCard
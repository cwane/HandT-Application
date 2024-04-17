import React from 'react'
import './PackageItemCard.css'

import ReportIcon from '../../assets/images/report-icon.png'
import EventGallery from '../../assets/images/event-gallery1.png'

const PackageItemCard = () => {
  return (
    <div class="package-event-item">
        <div class="package-image-wrap">
            <figure class="featured-image">
                <img src={EventGallery} alt="" />
            </figure>
            <span class="event-tag">hiking</span>
            </div>
            <div class="package-item-detail">
            <h3 class="entry-title">
                <a href="#">Phulchowki Hiking</a>
            </h3>
            <div class="entry-content">
                <p>Le lorem ipsum est, en imprimerie, une suite de mots sans signification utilisée à 
                titre provisoire. Le lorem ipsum est, </p>
            </div>
            <div class="package-item-info-detail">
                <ul>
                <li>
                    <span class="package-info-detail-icon">
                    <i class="fa fa-map-marker"></i>
                    </span>
                    Phulchoki, Kathmandu
                </li>
                <li>
                    <span class="package-info-detail-icon">
                    <i class="fa fa-clock-o"></i>
                    </span>
                    5 days
                </li>
                <li>
                    <span class="package-info-detail-icon">
                    <i class="fa fa-calendar-minus-o"></i>
                    </span>
                    June 30, 2023
                </li>
                </ul>
            </div>
            <div class="package-price-wrap">
                <span class="package-price-rate">Rs. 8,952</span>
                <span class="package-price-person">person</span>
            </div>
            <div class="package-report-wrap">
                <a href="#">
                    <img src={ReportIcon} alt="" />
                </a>
            </div>
        </div>
    </div>
  )
}

export default PackageItemCard
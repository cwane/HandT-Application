import React from 'react'
import './UpcomingEvents.css'

import PackageItemCard from '../PackageItemCard/PackageItemCard'

const UpcomingEvents = () => {
  return (
    <section class="upcoming-event-section">
        <div class="container">
            <header class="entry-header heading">
                <h2 class="entry-title">
                    Upcoming Events Near You
                </h2>
            </header>
            <div class="tab-wrapper">
                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <button class="nav-link active" data-bs-toggle="tab" data-bs-target="#nav-all">ALL</button>
                    <button class="nav-link" data-bs-toggle="tab" data-bs-target="#nav-trekking">TREKKING</button>
                    <button class="nav-link" data-bs-toggle="tab" data-bs-target="#nav-hiking" >HIKING</button>
                    <button class="nav-link" data-bs-toggle="tab" data-bs-target="#nav-camping" >CAMPING</button>
                    <button class="nav-link" data-bs-toggle="tab" data-bs-target="#nav-adventure" >aDVENTURE</button>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-all" role="tabpanel">
                        <div class="event-wrapper"> 
                            <PackageItemCard />
                            <PackageItemCard />
                            <PackageItemCard />
                            <PackageItemCard />
                            <PackageItemCard />
                        </div>
                        <div class="load-more-wrap">
                            <a href="#" class="box-button">load more</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
  )
}

export default UpcomingEvents
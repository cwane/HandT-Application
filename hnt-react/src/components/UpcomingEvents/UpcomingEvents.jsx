import React, { useState, useEffect } from 'react';

import './UpcomingEvents.css'


import PackageItemCard from '../PackageItemCard/PackageItemCard'
import axios from 'axios';

const UpcomingEvents = () => {

    const [events, setEvents] = useState([]);
    const [renderEvents, setRenderEvents] = useState([]);
    const [selectedCategory, setSelectedCategory] = useState("0"); // 0 for "ALL", 1 for "HIKING", 2 for "TREKKING"

    const categoryEnum = {
      "0": "All",
      "1": "Trekking",
      "2": "Hiking",
      "3": "Camping",
      "4": "Adventure"
    }
    
    const handleCategoryClick = (category) => {
        setSelectedCategory(category);
        if (category === "0") return setRenderEvents(events);
        else setRenderEvents(events.filter((event) => event.event_tag === categoryEnum[category]));
        
    };

    useEffect(() => {
      const dataFetch = async () => {
        const response = await axios.post(`${process.env.REACT_APP_ROOT_URL}/Front/Upcoming Events?category_id=0`);
        setEvents(response.data);
        setRenderEvents(response.data);
      }

      dataFetch();  
    }, []);
  
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
                      <button className={`nav-link ${selectedCategory === "0" ? 'active' : ''}`}
                        onClick={() => handleCategoryClick("0")}>ALL
                      </button>

                      <button className={`nav-link ${selectedCategory === "1" ? 'active' : ''}`}
                        onClick={() => handleCategoryClick("1")}>TREKKING
                      </button>

                      <button className={`nav-link ${selectedCategory === "2" ? 'active' : ''}`}
                        onClick={() => handleCategoryClick("2")}>HIKING
                      </button>

                      <button className={`nav-link ${selectedCategory === "3" ? 'active' : ''}`}
                        onClick={() => handleCategoryClick("3")}>CAMPING
                      </button>

                      <button className={`nav-link ${selectedCategory === "4" ? 'active' : ''}`}
                        onClick={() => handleCategoryClick("4")}>ADVENTURE
                      </button>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-all" role="tabpanel">
                        <div class="event-wrapper"> 
                            {renderEvents.map((event) => (
                              <PackageItemCard key={event.id} event={event} />
                            ))}
                            
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
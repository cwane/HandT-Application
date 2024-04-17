import React, { useState, useEffect } from 'react';
import Event1 from '../../assets/images/event1.png';


const EventListing = () => {
  const [category, setCategory] = useState('');
  const [location, setLocation] = useState('');
  const [date1, setDate1] = useState('');
  const [date2, setDate2] = useState('');
  const [price1, setPrice1] = useState('');
  const [price2, setPrice2] = useState('');
  const [tags, setTags] = useState('');
  const [text, setText] = useState('');
  const [filteredEvents, setFilteredEvents] = useState([]);


  const handleFilter = async () => {
    try {
      const apiUrl = 'https://localhost:7037/api/Front/Filter Events';
  
     
      const queryParams = new URLSearchParams({
        location: location,
        category_id: category,
        location: location,
        date1: date1,
        date2: date2,
        price1: price1,
        price2: price2,
        tags: tags,
        text: text,
      });
  
      
      const fullApiUrl = `${apiUrl}?${queryParams.toString()}`;
  
      const response = await fetch(fullApiUrl, {
        method: 'POST', 
        headers: {
          'Content-Type': 'application/json',
        },
      });
  
      if (!response.ok) {
        throw new Error(`API request failed with status ${response.status}`);
      }
  
      const data = await response.json();
      setFilteredEvents(data);
    } catch (error) {
      console.error('Error filtering events:', error.message);
    }
  };
  
useEffect(() => {
  handleFilter();
}, []);


return (
  <div id="content" className="site-content">
    <div id="primary" className="content-area">
      <main id="main" className="site-main">
        <section className="event-listing-page-wrap">
          <div className="container">
            <div className="row">
              <div className="col-sm-4">
                <div className="sidebar-filter-wrap">
                  <h3>Filter Events</h3>
                  <div className="filter-category-section">
                    <h4 className="sub-filter-title">Select Category</h4>
                    <div className="filter-category-content">
                      <select
                        className="select"
                        value={category}
                        onChange={(e) => setCategory(e.target.value)}
                      >
                        <option value="0">0</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                      </select>
                    </div>
                  </div>
                  <div className="filter-Location-section">
                    <h4 className="sub-filter-title">Location</h4>
                    <div className="filter-category-content">
                      <input
                        type="search"
                        name=""
                        placeholder="Search"
                        value={location}
                        onChange={(e) => setLocation(e.target.value)}
                      />
                    </div>
                  </div>
                  <div className="filter-date-section">
                    <h4 className="sub-filter-title">Start Date</h4>
                    <div className="filter-date-wrap-one">
                      <p>
                        <input
                          type="date"
                          name=""
                          placeholder="Pick a date range"
                          value={date1}
                          onChange={(e) => setDate1(e.target.value)}
                        />
                      </p>
                    </div>

                    <h4 className="sub-filter-title">End Date</h4>

                    <div className="filter-date-wrap-two">
                      <p>
                        <input
                          type="date"
                          name=""
                          placeholder="Pick a date range"
                          value={date2}
                          onChange={(e) => setDate2(e.target.value)}
                        />
                      </p>
                    </div>
                  </div>
                  <div className="filter-price-section">
                    <h4 className="sub-filter-title">Price</h4>
                    <div className="filter-price-wrap">
                      <p>
                        <input
                          type="text"
                          name=""
                          placeholder="Value"
                          value={price1}
                          onChange={(e) => setPrice1(e.target.value)}
                        />
                        <span className="price-tag">
                          <i className="fa fa-usd"></i>
                        </span>
                      </p>
                      <p>
                        <input
                          type="text"
                          name=""
                          placeholder="Value"
                          value={price2}
                          onChange={(e) => setPrice2(e.target.value)}
                        />
                        <span className="price-tag">
                          <i className="fa fa-usd"></i>
                        </span>
                      </p>
                    </div>
                  </div>
                  <div className="filter-Location-section">
                    <h4 className="sub-filter-title">Tags</h4>
                    <div className="filter-category-content">
                      <input
                        type="search"
                        name=""
                        placeholder="Search"
                        value={tags}
                        onChange={(e) => setTags(e.target.value)}
                      />
                    </div>
                  </div>
                  <div className="filter-search-section">
                    <h4 className="sub-filter-title">Search Text</h4>
                    <div className="filter-search-wrap">
                      
                        <input
                          type="search"
                          name=""
                          placeholder="Search"
                          value={text}
                          onChange={(e) => setText(e.target.value)}
                        />

                      </div>
                    </div>
                    <div class="sidebar-filter-btn-wrap">
                      <button onClick={handleFilter} class="box-button only-border-btn">Reset</button>
                      <button onClick={handleFilter} class="box-button">Filter</button>
                    </div>
                  </div>
                </div>
                <div class="col-sm-8">
                <div className="event-item-wrapper">
                  {filteredEvents.map((event) => (
            <div className="package-event-item" key={event.id}>
                      <div class="package-image-wrap">
                        <figure class="featured-image">
                          <img src={Event1} alt="" />
                        </figure>
                      </div>
                      <div class="package-item-detail">
                        <h3 class="entry-title">
                          <a href="#" tabindex="0">{event.event_title}</a>
                        </h3>
                        <div class="entry-content">
                          <p>{event.event_desc} </p>
                        </div>
                        <div class="package-item-info-detail">
                          <ul>
                            <li>
                              <span class="package-info-detail-icon">
                                <i class="fa fa-map-marker"></i>
                              </span>
                              {event.destination_location}
                            </li>
                            <li>
                              <span class="package-info-detail-icon">
                                <i class="fa fa-clock-o"></i>
                              </span>
                              {event.days} days
                            </li>
                          </ul>
                        </div>
                        <div class="package-price-wrap">
                          <span class="package-price-rate">Rs. {event.cost_per_person}</span>
                          <span class="package-price-person">person</span>
                        </div>
                        <div class="view-event-btn-wrap">
                          <a href="single-event" class="box-button">view event</a>
                        </div>
                      </div>
                    </div>
                    ))}
                   
                  </div>
                </div>
              </div>
            </div>
          </section>
        </main>
        
      </div>
     
    </div>
  );
}

export default EventListing;

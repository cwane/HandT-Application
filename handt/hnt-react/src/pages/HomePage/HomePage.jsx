import React from 'react'
import Header from '../../components/Header/Header'
import Banner from '../../components/Banner/Banner'
import FeaturedEvents from '../../components/FeaturedEvents/FeaturedEvents'
import UpcomingEvents from '../../components/UpcomingEvents/UpcomingEvents'
import SpecialOffer from '../../components/SpecialOffer/SpecialOffer'
import TravelPartner from '../../components/TravelPartner/TravelPartner'
import Footer from '../../components/Footer/Footer'

const HomePage = () => {
  return (
    <div className="hfeed site" id="page">

      <Header />
      
      <div id="content" className="site-content">
        <div id="primary" className="content-area">
          <main id="main" className="site-main">

            <Banner />
            <FeaturedEvents />
            <UpcomingEvents />
            <SpecialOffer />
            <TravelPartner />

          </main>
          {/* #main */}
        </div>
        {/* #primary */}
      </div>
      {/* .site-content */}

      <Footer />
    </div>
  )
}

export default HomePage
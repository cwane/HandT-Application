import React, { useState } from 'react'

import Banner from '../../components/Banner/Banner'
import FeaturedEvents from '../../components/FeaturedEvents/FeaturedEvents'
import UpcomingEvents from '../../components/UpcomingEvents/UpcomingEvents'
import SpecialOffer from '../../components/SpecialOffer/SpecialOffer'
import TravelPartner from '../../components/TravelPartner/TravelPartner'

const HomePage = () => {
  return (
    <>
      <Banner />
      <FeaturedEvents />
      <UpcomingEvents />
      <SpecialOffer />
      <TravelPartner />
    </>
  )
}

export default HomePage
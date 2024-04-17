using HandT_Api_Layer.DomainInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandT_Api_Layer.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FrontController : ControllerBase
    {
        private readonly IFrontRepo _frontRepo;
        public FrontController(IFrontRepo frontRepo)
        {
            _frontRepo = frontRepo;
        }

        [HttpGet]
        [Route("Featured-Events")]
        public async Task<IActionResult> GetFeaturesEvents()
        {
            try
            {
                var response = await _frontRepo.getFeaturedEvents();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("Event-Detail")]
        public async Task<IActionResult> GetFrontEvent(int event_id)
        {
            try
            {
                var response = await _frontRepo.GetFrontEventDetails(event_id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[httppost]
        //[route("upcoming-events")]
        //public async task<iactionresult> upcomingevents(events events)
        //{
        //    try
        //    {
        //        var resp = await _eventrepo.upcomingevents(events);
        //        return ok(resp);
        //    }
        //    catch (exception ex)
        //    {
        //        return statuscode(500, ex.message);
        //    }
        //}

        [HttpPost]
        [Route("Upcoming Events")]
        public async Task<IActionResult> UpcomingEvents(int category_id)
        {
            try
            {
                var events = await _frontRepo.UpcomingEvents(category_id);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Nearby Events")]
        public async Task<IActionResult> NearbyEvents(decimal latitude, decimal longitude)
        {
            try
            {
                var events = await _frontRepo.NearbyEvents(latitude, longitude);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [HttpPost]
        // [Route("Google-Map")]
        // public IActionResult DisplayMap()
        // {
        //   return View("~/GoogleMap/Index.cshtml");
        // }



        [HttpPost]
        [Route("Filter Events")]
        public async Task<IActionResult> filterEvents(int? category_id, string? location, decimal? price1,decimal? price2, string? tags, string? text, DateTime? date1, DateTime? date2)
        {
            try
            {
                var events = await _frontRepo.filterEvents(category_id,location,price1,price2,tags,text,date1,date2);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}

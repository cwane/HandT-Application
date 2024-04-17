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


    }
}

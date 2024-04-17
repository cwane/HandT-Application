using HandT_Test.Authentication;
using HandT_Test.DbContext;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_Mysql.DomainInterface;
using HandT_Test_Mysql.DomainRepository;
using HandT_Test_PG.Authentication;
using HandT_Test_PG.DomainEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace HandT_Test_PG.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Participator,Creator")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _databaseContext;
        private readonly IEventRepo _eventRepo;
        private readonly ICommonRepo _commonRepo;
        private IHttpContextAccessor _httpContextAccessor;

        public EventsController(UserManager<ApplicationUser> userManager,
            DatabaseContext databaseContext,
            IEventRepo eventRepo, ICommonRepo commonRepo
            , IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _databaseContext = databaseContext;
            _eventRepo = eventRepo;
            _commonRepo = commonRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("Book-Event")]
        public async Task<IActionResult> BookEvent(EventParticipant eventParticipant)
        {
            try
            {
                var accessToken = Request.Headers["Authorization"];
                var profileData = await _commonRepo.GetTokenDetail(accessToken);
                eventParticipant.user_id = profileData.UserName;
                var response = await _eventRepo.BookEvent(eventParticipant);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        [Route("Get-EventNo-Out")]
        public async Task<IActionResult> GetEventNoOut(Events events)
        {
            try
            {
                var resp = await _eventRepo.GetEventNoOut(events);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Get-Event-Out")]
        public async Task<IActionResult> GetEventOut(Events events)
        {
            try
            {
                var resp = await _eventRepo.GetEventOut(events);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

       
        [HttpPost]
        [Route("Add-Events")]
        public async Task<IActionResult> AddEvent(Events events)
        {
            try
            {
                var pro = _httpContextAccessor;
                var resp = await _eventRepo.AddEvents(events);
                return Ok("added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Add-Event-Days")]
        public async Task<IActionResult> AddEventDays(int event_id, [FromBody] List<EventDay> eventDays)
        {
            try
            {
                var resp = await _eventRepo.AddEventDays(event_id, eventDays);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Add-Event-Photos")]
        public async Task<IActionResult> AddEventPhotos(int event_id, [FromBody] List<EventPhoto> eventPhotos)
        {
            try
            {
                var resp = await _eventRepo.AddEventPhotos(event_id, eventPhotos);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("View-Events")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = await _eventRepo.getEvents();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Add-Event")]
        public async Task<IActionResult> SaveNewEvent(Events events)
        {
            Events eventData = new Events();
            //TokenModel tokenModel = new TokenModel();
            //var accessToken = Request.Headers["Authorization"];
            //var tokeninfo = accessToken.ToString().Split(' ');
            //var jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokeninfo[1]);
            //string userdetails = "";
            //List<string> claims = new List<string>();

            //foreach (var item in jwt.Claims)
            //{
            //    userdetails += item.Value.ToString() + ", ";
            //    claims.Add(item.Value.ToString());
            //}

            //var token_usrname = claims[0];
            //var user_details = await _userManager.FindByNameAsync(token_usrname);
            //var user_id = user_details.Id;
            //eventData.UserId = "gaurab";
            //eventData.EventTitle = events.EventTitle;
            //eventData.CategoryId = 1;
            //eventData.DestinationLocation = events.DestinationLocation;
            //eventData.PickupLocation = events.PickupLocation;
            //eventData.CoverImage = events.CoverImage;
            //eventData.IsSingleRecurring = events.IsSingleRecurring;
            //eventData.StartDate = events.StartDate;
            //eventData.StartTime = events.StartTime;
            //eventData.EndDate = events.EndDate;
            //eventData.EndTime = events.EndTime;
            //eventData.NoOfPeople = events.NoOfPeople;
            //eventData.IsPaidFree = events.IsPaidFree;
            //eventData.CostPerPerson = events.CostPerPerson;
            //eventData.PaymentOption = events.PaymentOption;
            //eventData.PubishTime = events.PubishTime;
            //eventData.Draft = events.Draft;
            //eventData.SalesStart = events.SalesStart;
            //eventData.IsSaved = events.IsSaved;
            //eventData.EnteredBy = events.EnteredBy;
            //eventData.EnteredDate = events.EnteredDate;
            //eventData.UpdatedBy = events.UpdatedBy;
            //eventData.UpdatedDate = events.UpdatedDate;

            return Ok("Data Saved");
        }
    }
}

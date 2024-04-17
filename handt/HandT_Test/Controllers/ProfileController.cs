using HandT_Test.Authentication;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_Mysql.DomainInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HandT_Test_Mysql.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Participator)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ICommonRepo _commonRepo;
        private readonly IProfileRepo _profileRepo;


        public ProfileController(ICommonRepo commonRepo, IProfileRepo profileRepo)
        {
            _commonRepo = commonRepo;
            _profileRepo = profileRepo;
        }

        [HttpPost]
        [Route("edit-profile")]
        public async Task<IActionResult> EditProfile(Profile profile)
        {
            var accessToken = Request.Headers["Authorization"];
            var profileData = await _commonRepo.GetTokenDetail(accessToken);
            profile.UserName = profileData.UserName;
            var response = await _profileRepo.UpdateProfile(profile);
            return Ok("hello world");
        }

        [HttpGet]
        [Route("test-profile")]
        public async Task<IActionResult> TestProfile()
        {
            return Ok("hello world");
        }

        [HttpPost]
        [Route("user-connect")]
        public async Task<IActionResult> UserConnect(UserConnection userConnection)
        {
            try
            {
                var accessToken = Request.Headers["Authorization"];
                var profileData = await _commonRepo.GetTokenDetail(accessToken);
                userConnection.followed_by = profileData.UserName;
                var response = await _profileRepo.UserConnection(userConnection);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("post-review")]
        public async Task<IActionResult> PostReview(UserRating userRating)
        {
            try
            {
                var accessToken = Request.Headers["Authorization"];
                var profileData = await _commonRepo.GetTokenDetail(accessToken);
                var response = await _profileRepo.PostRating(userRating);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("User-Profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"];
                var profileData = await _commonRepo.GetTokenDetail(accessToken);
                var response = await _profileRepo.GetUserProfile(profileData.UserName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Profile-Count")]
        public async Task<IActionResult> GetProfileParamCount()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"];
                var profileData = await _commonRepo.GetTokenDetail(accessToken);
                var response = await _profileRepo.GetProfileParamCount(profileData.UserName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("My-Dashboard")]
        public async Task<IActionResult> GetMyDashboard()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"];
                var profileData = await _commonRepo.GetTokenDetail(accessToken);
                var response = await _profileRepo.GetUserDashboard(profileData.UserName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

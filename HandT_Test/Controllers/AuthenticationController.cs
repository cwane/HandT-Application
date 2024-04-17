using HandT_Api_Layer.DomainEntities;
using HandT_Test_Mysql.DomainEntities;
using Microsoft.AspNetCore.Mvc;
using HandT_Api_Layer.DomainInterface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HandT_Api_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepo _authenticationRepo;
        public AuthenticationController(IAuthenticationRepo authenticationRepo)
        {
            _authenticationRepo = authenticationRepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<object>>> Register(UserRegistrationRequest request)
        {
            var response = await _authenticationRepo.Register(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<object>>> Login(UserLoginRequest request)
        {
            var response = await _authenticationRepo.Login(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("verify/{token}")]
        public async Task<ActionResult<ServiceResponse<string>>> Verify(string token)
        {
            var response = await _authenticationRepo.Verify(token);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ForgotPassword(UserRequestForgotPassword request)
        {
            var response = await _authenticationRepo.ForgotPassword(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ResetPassword(ResetPasswordRequest request)
        {
            var response = await _authenticationRepo.ResetPassword(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("register-internal-user")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ServiceResponse<string>>> RegisterInternalUser(UserRegistrationRequest request)
        {
            var Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
            if (Role == "Admin")
            {
                var response = await _authenticationRepo.RegisterInternalUser(request);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            } else
            {
                return BadRequest("Unauthorized");
            }
        }

        [HttpPost("change-password")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ServiceResponse<string>>> ChangePassword(UserPasswordChangeRequest request)
        {
            var username = User?.Identity?.Name;
            var response = await _authenticationRepo.ChangePassword(request, username);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("update-individual-user-detail")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateIndividualUserDetail([FromForm]IndividualUserProfileUpdateRequest request, IFormFile? pictureFile)
        {
            var username = User?.Identity?.Name;
            var response = await _authenticationRepo.UpdateIndividualUserDetail(request, pictureFile, username);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpGet("get-user-detail")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ServiceResponse<object>>> GetUserDetail()
        {
            try
            {
                var username = User?.Identity?.Name;
                var response = await _authenticationRepo.GetUserDetail( username);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Internal server error",
                    Data = null
                });
            }
        }



        [HttpPost("update-corporate-user-detail")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateCorporateUserDetail([FromForm] CorporateUserProfileUpdateRequest request, IFormFile? pictureFile)
        {
            var username = User?.Identity?.Name;
            var response = await _authenticationRepo.UpdateCorporateUserDetail(request, pictureFile, username);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

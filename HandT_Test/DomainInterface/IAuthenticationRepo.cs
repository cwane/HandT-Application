using HandT_Api_Layer.DomainEntities;
using HandT_Test_Mysql.DomainEntities;
using Microsoft.AspNetCore.Mvc;

namespace HandT_Api_Layer.DomainInterface
{
    public interface IAuthenticationRepo
    {
        Task<ServiceResponse<object>> Register(UserRegistrationRequest request);
        Task<ServiceResponse<object>> Login(UserLoginRequest request);
        Task<ServiceResponse<string>> Verify(string token);
        Task<ServiceResponse<string>> ForgotPassword(UserRequestForgotPassword request);
        Task<ServiceResponse<string>> ResetPassword(ResetPasswordRequest request);
        Task<ServiceResponse<string>> RegisterInternalUser(UserRegistrationRequest request);
        Task<ServiceResponse<string>> ChangePassword(UserPasswordChangeRequest request, string username);
        Task<ServiceResponse<string>> UpdateIndividualUserDetail(IndividualUserProfileUpdateRequest request, IFormFile? pictureFile, string username);
        Task<ServiceResponse<string>> UpdateCorporateUserDetail(CorporateUserProfileUpdateRequest request, IFormFile? pictureFile, string username);
        Task<ServiceResponse<object>> GetUserDetail(string username);

    }
}
